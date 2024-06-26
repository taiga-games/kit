using System;

namespace TaigaGames.Kit.ECS
{
    [Serializable]
    public struct Entity : IEntity
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
            OnComponentAdded = null;
            OnComponentChanged = null;
            OnComponentRemoved = null;
        }

        public event EntityComponentAdded OnComponentAdded;
        public event EntityComponentChanged OnComponentChanged;
        public event EntityComponentRemoved OnComponentRemoved;

        public uint GetId()
        {
            return Id;
        }

        public int GetEcsEntityId()
        {
            return EcsEntityId;
        }

        public void Set<T>(T component) where T : struct
        {
            if (_ecsWorld.GetPool<T>().Has(EcsEntityId))
            {
                _ecsWorld.GetPool<T>().Get(EcsEntityId) = component;
                OnComponentChanged?.Invoke(this, typeof(T));
            }
            else
            {
                _ecsWorld.GetPool<T>().Add(EcsEntityId) = component;
                OnComponentAdded?.Invoke(this, typeof(T));
            }
        }

        public void Set(Type componentType, object component)
        {
            var pool = _ecsWorld.GetPoolByType(componentType);
            if (pool.Has(EcsEntityId))
            {
                pool.SetRaw(EcsEntityId, component);
                OnComponentChanged?.Invoke(this, componentType);
            }
            else
            {
                pool.AddRaw(EcsEntityId, component);
                OnComponentAdded?.Invoke(this, componentType);
            }
        }

        public T Get<T>() where T : struct
        {
            return _ecsWorld.GetPool<T>().Get(EcsEntityId);
        }

        public ref T GetRef<T>() where T : struct
        {
            return ref _ecsWorld.GetPool<T>().Get(EcsEntityId);
        }

        public object Get(Type componentType)
        {
            return _ecsWorld.GetPoolByType(componentType).GetRaw(EcsEntityId);
        }

        public bool Has<T>() where T : struct
        {
            return _ecsWorld.GetPool<T>().Has(EcsEntityId);
        }

        public bool Has(Type componentType)
        {
            return _ecsWorld.GetPoolByType(componentType).Has(EcsEntityId);
        }

        public void Del<T>() where T : struct
        {
            _ecsWorld.GetPool<T>().Del(EcsEntityId);
            OnComponentRemoved?.Invoke(this, typeof(T));
        }

        public void Del(Type componentType)
        {
            _ecsWorld.GetPoolByType(componentType).Del(EcsEntityId);
            OnComponentRemoved?.Invoke(this, componentType);
        }

        public void Destroy()
        {
            _entityRegistry.Unregister(Id);
            _ecsWorld.DelEntity(EcsEntityId);
        }

        public void Clone(IEntity toEntity)
        {
            var entityId = toEntity.GetId();
            _ecsWorld.CopyEntity(EcsEntityId, toEntity.GetEcsEntityId());
            toEntity.GetRef<EntityId>().Value = entityId;
        }

        public bool TryGetName(out string name)
        {
            if (!Has<EntityName>())
            {
                name = null;
                return false;
            }
            
            name = Get<EntityName>().Value;
            return true;
        }

        public string GetName()
        {
            return TryGetName(out var name) ? name : $"Entity {Id}";
        }

        public void SetName(string name)
        {
            Set(new EntityName { Value = name });
        }

        public static implicit operator uint(Entity entity) => entity.Id;
        
        public static implicit operator int(Entity entity) => entity.EcsEntityId;
        
        public static implicit operator EntityId(Entity entity) => new EntityId { Value = entity.Id };

        public override string ToString()
        {
            return GetName();
        }
    }
}