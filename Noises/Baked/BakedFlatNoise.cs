using System;
using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;

namespace TaigaGames.Noises
{
    [BurstCompile(FloatPrecision.Standard, FloatMode.Fast, CompileSynchronously = true)]
    public struct BakedFlatNoise : INoiseProvider, IDisposable
    {
        private readonly int2 _size;
        [ReadOnly] private NativeArray<float> _values;

        public BakedFlatNoise(int2 size, NativeArray<float> values)
        {
            _size = size;
            _values = values;
        }

        public float GetNoise(float x, float y)
        {
            var xInt = (int) math.remap(0f, _size.x - 1f, 0f, _size.x - 1f, x);
            var yInt = (int) math.remap(0f, _size.y - 1f, 0f, _size.y - 1f, y);
            return _values[xInt + yInt * _size.x];
        }

        public float GetNoise(float x, float y, float z)
        {
            return GetNoise(x, y);
        }

        public void Dispose()
        {
            _values.Dispose();
        }
    }
}