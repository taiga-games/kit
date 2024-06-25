using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace TaigaGames.Noises
{
    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    public struct BakedVolumetricNoise : INoiseProvider, IDisposable
    {
        private readonly int3 _size;
        [ReadOnly] private NativeArray<float> _values;

        public BakedVolumetricNoise(int3 size, NativeArray<float> values)
        {
            _size = size;
            _values = values;
        }

        public float GetNoise(float x, float y)
        {
            return GetNoise(x, y, 0);
        }

        public float GetNoise(float x, float y, float z)
        {
            var xInt = (int) math.remap(0, _size.x, 0, _size.x, x);
            var yInt = (int) math.remap(0, _size.y, 0, _size.y, y);
            var zInt = (int) math.remap(0, _size.z, 0, _size.z, z);
            return _values[xInt + yInt * _size.x + zInt * _size.x * _size.y];
        }

        public void Dispose()
        {
            _values.Dispose();
        }
    }
}