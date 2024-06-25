namespace TaigaGames.Kit.ECS
{
    public interface IEcsDestroySystem : IEcsSystem
    {
        void Destroy(IEcsSystems systems);
    }
}