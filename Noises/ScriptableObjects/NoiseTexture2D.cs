using System;
using TaigaGames.Kit;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace TaigaGames.Noises
{
    [CreateAssetMenu(fileName = "NoiseTexture", menuName = "TaigaGames/Noises/NoiseTexture", order = 0)]
    public class NoiseTexture2D : ScriptableObject, IFlatNoiseBaker
    {
        [field: SerializeField] public Texture2D Texture { get; private set; }
        
        public BakedFlatNoise BakeFlatNoise(int seed, int2 size, float2 offset, float zoom = 1f)
        {
            if (!Texture.isReadable)
                throw new Exception($"Texture \"{Texture.name}\" in \"{name}\" is not readable.");

            var texture = TextureScaler.Scale(Texture, size.x, size.y);
            var values = new NativeArray<float>(size.x * size.y, Allocator.Persistent);
            
            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
                values[y * size.x + x] = texture.GetPixel(x, y).r;
            
            return new BakedFlatNoise(seed, values);
        }
        
        public static BakedFlatNoise BakeFlatNoise(Texture2D tex, int2 size)
        {
            if (!tex.isReadable)
                throw new Exception($"Texture \"{tex.name}\" is not readable.");

            var texture = TextureScaler.Scale(tex, size.x, size.y);
            var values = new NativeArray<float>(size.x * size.y, Allocator.Persistent);
            
            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
                values[y * size.x + x] = texture.GetPixel(x, y).r;
            
            return new BakedFlatNoise(0, values);
        }
    }
}