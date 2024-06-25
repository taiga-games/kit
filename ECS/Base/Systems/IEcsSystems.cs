using System.Collections.Generic;

namespace TaigaGames.Kit.ECS
{
    public interface IEcsSystems
    {
        T GetShared<T>() where T : class;
        IEcsSystems AddWorld(EcsWorld world, string name);
        EcsWorld GetWorld(string name = null);
        Dictionary<string, EcsWorld> GetAllNamedWorlds();
        IEcsSystems Add(IEcsSystem system);
        List<IEcsSystem> GetAllSystems();
        void Init();
        void Run();
        void Destroy();
    }
}