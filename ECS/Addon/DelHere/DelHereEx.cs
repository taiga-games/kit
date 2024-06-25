namespace TaigaGames.Kit.ECS
{
    public static class DelHereEx
    {
        public static IEcsSystems DelHere<T>(this IEcsSystems ecsSystems) where T : struct
        {
            ecsSystems.Add(new DelHereSystem<T>());
            return ecsSystems;
        }
    }
}