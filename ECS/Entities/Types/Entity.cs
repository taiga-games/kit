using System;

namespace TaigaGames.Kit.ECS
{
    [Serializable]
    public readonly struct Entity : IEntity
    {
        public readonly uint Id;
        [NonSerialized] public readonly int EcsEntityId;
        
        [NonSerialized] private readonly EcsWorld _ecsWorld;
        [NonSerialized] private readonly EntityRegistry _entityRegistry;

        public Entity(uint id, int ecsEntityId, EcsWorld ecsWorld, EntityRegistry entityRegistry)
        {
            Id = id;
            EcsEntityId = ecsEntityId;
            _ecsWorld = ecsWorld;
            _entityRegistry = entityRegistry;
        }

        public uint GetId()
        {
            return Id;
        }

        public int GetEcsEntityId()
        {
            return EcsEntityId;
        }

        public ref T Add<T>() where T : struct
        {
            return ref _ecsWorld.GetPool<T>().Add(EcsEntityId);
        }

        public ref T Get<T>() where T : struct
        {
            return ref _ecsWorld.GetPool<T>().Get(EcsEntityId);
        }

        public ref T GetOrAdd<T>() where T : struct
        {
            if (Has<T>())
                return ref Get<T>();
            return ref Add<T>();
        }

        public bool Has<T>() where T : struct
        {
            return _ecsWorld.GetPool<T>().Has(EcsEntityId);
        }

        public void Del<T>() where T : struct
        {
            _ecsWorld.GetPool<T>().Del(EcsEntityId);
        }

        public void Destroy()
        {
            _entityRegistry.Unregister(Id);
            _ecsWorld.DelEntity(EcsEntityId);
        }

        public static implicit operator uint(Entity entity) => entity.Id;
    }
}