namespace TaigaGames.Kit.ECS
{
    public interface IEcsWorldEventListener
    {
        void OnEntityCreated(int entity);
        void OnEntityChanged(int entity, short poolId, bool added);
        void OnEntityDestroyed(int entity);
        void OnFilterCreated(EcsFilter filter);
        void OnWorldResized(int newSize);
        void OnWorldDestroyed(EcsWorld world);
    }
}