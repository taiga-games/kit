namespace TaigaGames.Kit.ECS
{
    public interface IEcsUpdateFeature : IEcsFeature
    {
        void SetupUpdateSystems(IEcsSystems systems);
    }

    public interface IEcsLateUpdateFeature : IEcsFeature
    {
        void SetupLateUpdateSystems(IEcsSystems systems);
    }

    public interface IEcsFixedUpdateFeature : IEcsFeature
    {
        void SetupFixedUpdateSystems(IEcsSystems systems);
    }

    public interface IEcsFeature
    {
    }
}