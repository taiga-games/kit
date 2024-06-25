namespace TaigaGames.Kit.ECS
{
    public interface IEcsPostDestroySystem : IEcsSystem
    {
        void PostDestroy(IEcsSystems systems);
    }
}