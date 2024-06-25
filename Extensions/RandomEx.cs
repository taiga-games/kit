using System;

namespace TaigaGames.Kit
{
    public static class RandomEx
    {
        public static float NextFloat(this Random random)
        {
            return (float) random.NextDouble();
        }
    }
}