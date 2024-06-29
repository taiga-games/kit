using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace TaigaGames.Noises
{
    [CreateAssetMenu(fileName = "NoiseGenerationSettings", menuName = "TaigaGames/Noises/NoiseGenerationSettings")]
    public class NoiseGenerationSettings : ScriptableObject, IFlatNoiseBaker, IVolumetricNoiseBaker
    {
        [SerializeField] private int _seed = 1337;
        [SerializeField] private float _frequency = 0.01f;
        [SerializeField] private FastNoiseLite.NoiseType _noiseType = FastNoiseLite.NoiseType.OpenSimplex2;
        [SerializeField] private FastNoiseLite.RotationType3D _rotationType3D = FastNoiseLite.RotationType3D.None;

        [Space]
        [SerializeField] private FastNoiseLite.FractalType _fractalType = FastNoiseLite.FractalType.None;
        [SerializeField] private int _octaves = 3;
        [SerializeField] private float _lacunarity = 2f;
        [SerializeField] private float _gain = 0.5f;
        [SerializeField] private float _weightedStrength = 0f;
        [SerializeField] private float _pingPongStrength = 2f;

        [Space]
        [SerializeField] private FastNoiseLite.CellularDistanceFunction _cellularDistanceFunction = FastNoiseLite.CellularDistanceFunction.EuclideanSq;
        [SerializeField] private FastNoiseLite.CellularReturnType _cellularReturnType = FastNoiseLite.CellularReturnType.Distance;
        [SerializeField] private float _cellularJitterModifier = 1f;

        [Space]
        [SerializeField] private FastNoiseLite.DomainWarpType _domainWarpType = FastNoiseLite.DomainWarpType.OpenSimplex2;
        [SerializeField] private FastNoiseLite.TransformType3D _warpTransformType3D = FastNoiseLite.TransformType3D.DefaultOpenSimplex2;
        [SerializeField] private float _domainWarpAmp = 1f;
        
        [Space]
        [SerializeField] private NoisePostProcessingStep[] _postProcessingSteps;
        
        public float GetNoise(int seed, float x, float y, float zoom = 1f)
        {
            var noiseGenerator = GetFastNoiseLite(_seed + seed, zoom);
            var noise = noiseGenerator.GetNoise(x, y);
            for (var i = 0; i < _postProcessingSteps.Length; i++) 
                noise = math.clamp(_postProcessingSteps[i].Execute(noise), 0, 1);
            return noise;
        }
        
        public BakedFlatNoise BakeFlatNoise(int seed, int2 size, float2 offset, float zoom = 1f)
        {
            var noiseGenerator = GetFastNoiseLite(_seed + seed, zoom);
            var values = new NativeArray<float>(size.x * size.y, Allocator.Persistent);
            
            for (var x = offset.x; x < offset.x + size.x; x++)
            for (var y = offset.y; y < offset.y + size.y; y++)
            {
                var tx = x;
                var ty = y;
                if (_domainWarpType != FastNoiseLite.DomainWarpType.None) 
                    noiseGenerator.DomainWarp(ref tx, ref ty);
                
                var noise = math.remap(-1, 1, 0, 1, noiseGenerator.GetNoise(tx, ty));
                
                for (var i = 0; i < _postProcessingSteps.Length; i++) 
                    noise = math.clamp(_postProcessingSteps[i].Execute(noise), 0, 1);
                
                var index = (int)(x - offset.x) + (int)(y - offset.y) * size.x;
                values[index] = noise;
            }
            
            var bakedNoise = new BakedFlatNoise(size, values);
            return bakedNoise;
        }

        public BakedFlatNoise BakeFlatNoise(int seed, Vector2Int size, Vector2 offset, float zoom = 1)
        {
            return BakeFlatNoise(seed, new int2(size.x, size.y), new float2(offset.x, offset.y), zoom);
        }

        public BakedVolumetricNoise BakeVolumetricNoise(int seed, int3 size, float3 offset, float zoom = 1f)
        {
            var noiseGenerator = GetFastNoiseLite(_seed + seed, zoom);
            var values = new NativeArray<float>(size.x * size.y * size.z, Allocator.Persistent);
            
            for (var x = offset.x; x < offset.x + size.x; x++)
            for (var y = offset.y; y < offset.y + size.y; y++)
            for (var z = offset.z; z < offset.z + size.z; z++)
            {
                var tx = 0f;
                var ty = 0f;
                var tz = 0f;
                if (_domainWarpType != FastNoiseLite.DomainWarpType.None) 
                    noiseGenerator.DomainWarp(ref tx, ref ty, ref tz);
                var index = (int)(x - offset.x) + (int)(y - offset.y) * size.x + (int)(z - offset.z) * size.x * size.y;
                values[index] = noiseGenerator.GetNoise(tx, ty, tz);
            }
            
            var bakedNoise = new BakedVolumetricNoise(size, values);
            return bakedNoise;
        }

        public BakedVolumetricNoise BakeVolumetricNoise(int seed, Vector3Int size, Vector3 offset, float zoom = 1)
        {
            return BakeVolumetricNoise(seed, new int3(size.x, size.y, size.z), new float3(offset.x, offset.y, offset.z), zoom);
        }

        private FastNoiseLite GetFastNoiseLite(int seed, float zoom = 1f)
        {
            var noiseGenerator = new FastNoiseLite();
            noiseGenerator.SetSeed(seed);
            noiseGenerator.SetNoiseType(_noiseType);
            noiseGenerator.SetFrequency(_frequency / zoom);
            noiseGenerator.SetRotationType3D(_rotationType3D);
            noiseGenerator.SetFractalType(_fractalType);
            noiseGenerator.SetFractalOctaves(_octaves);
            noiseGenerator.SetFractalLacunarity(_lacunarity);
            noiseGenerator.SetFractalGain(_gain);
            noiseGenerator.SetFractalWeightedStrength(_weightedStrength);
            noiseGenerator.SetFractalPingPongStrength(_pingPongStrength);
            noiseGenerator.SetCellularDistanceFunction(_cellularDistanceFunction);
            noiseGenerator.SetCellularReturnType(_cellularReturnType);
            noiseGenerator.SetCellularJitter(_cellularJitterModifier);
            noiseGenerator.SetDomainWarpType(_domainWarpType);
            noiseGenerator.SetDomainWarpAmp(_domainWarpAmp);
            return noiseGenerator;
        }
        
        [Serializable]
        public class NoisePostProcessingStep
        {
            public bool Enabled = true;
            public NoisePostProcessingType Type;
            public float Value1;
            public float Value2;
            public AnimationCurve Curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

            public float Execute(float input)
            {
                if (!Enabled)
                    return input;

                return Type switch
                {
                    NoisePostProcessingType.None => input,
                    NoisePostProcessingType.Power => Mathf.Pow(input, Value1),
                    NoisePostProcessingType.Invert => 1 - input,
                    NoisePostProcessingType.Abs => Mathf.Abs(input),
                    NoisePostProcessingType.Clamp => Mathf.Clamp(input, Value1, Value2),
                    NoisePostProcessingType.Normalize => Mathf.InverseLerp(Value1, Value2, input),
                    NoisePostProcessingType.Scale => input * Value1,
                    NoisePostProcessingType.Offset => input + Value1,
                    NoisePostProcessingType.Curve => Curve.Evaluate(input),
                    NoisePostProcessingType.SmoothStep => Mathf.SmoothStep(Value1, Value2, input),
                    NoisePostProcessingType.Step => input > Value1 ? 1 : 0,
                    NoisePostProcessingType.Lerp => Mathf.Lerp(Value1, Value2, input),
                    _ => input
                };
            }

            public enum NoisePostProcessingType
            {
                None,
                Power,
                Invert,
                Abs,
                Clamp,
                Normalize,
                Scale,
                Offset,
                Curve,
                SmoothStep,
                Step,
                Lerp
            }
        }
    }
}