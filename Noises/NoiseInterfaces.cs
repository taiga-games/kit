using Unity.Mathematics;

namespace TaigaGames.Noises
{
    public interface INoiseProvider
    {
        float GetNoise(float x, float y);
        float GetNoise(float x, float y, float z);
    }

    public interface IFlatNoiseBaker
    {
        BakedFlatNoise BakeFlatNoise(int seed, int2 size, float2 offset, float zoom = 1f);
    }

    public interface IVolumetricNoiseBaker
    {
        BakedVolumetricNoise BakeVolumetricNoise(int seed, int3 size, float3 offset, float zoom = 1f);
    }
}