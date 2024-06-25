namespace TaigaGames.Kit.ECS
{
    public interface IEcsRunSystem : IEcsSystem
    {
        void Run(IEcsSystems systems);
    }
}