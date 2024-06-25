namespace TaigaGames.Kit.ECS
{
    public interface IEcsPreInitSystem : IEcsSystem
    {
        void PreInit(IEcsSystems systems);
    }
}