using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaigaGames.Kit.ECS
{
    public class EntityRegistry : IEntityRegistry
    {
        private readonly EcsWorld _world;
        
        private EcsPool<EntityId> _entityIdPool;
        
        private readonly Dictionary<uint, Entity> _idToEntity = new Dictionary<uint, Entity>();
        private uint _lastId = 0;

        public EntityRegistry(EcsWorld world)
        {
            _world = world;
            _entityIdPool = _world.GetPool<EntityId>();
        }
        
        public uint GetNextId()
        {
            return _lastId++;
        }
        
        public IEntity RegisterNew()
        {
            var id = GetNextId();
            return Register(id, _world.NewEntity());
        }
        
        public IEntity RegisterNew(int ecsEntityId)
        {
            var id = GetNextId();
            return Register(id, ecsEntityId);
        }
        
        public IEntity Register(uint id, int ecsEntityId)
        {
            if (_entityIdPool.Has(ecsEntityId))
                _entityIdPool.Get(ecsEntityId).Value = id;
            else
                _entityIdPool.Add(ecsEntityId).Value = id;

            var entity = new Entity(id, ecsEntityId, _world, this);
            _idToEntity.Add(id, entity);
            return entity;
        }
        
        public void Unregister(uint id)
        {
            _idToEntity.Remove(id);
        }

        public IEntity GetEntity(uint id)
        {
            return _idToEntity[id];
        }

        public bool TryGetEntity(uint id, out IEntity entity)
        {
            var exists = _idToEntity.TryGetValue(id, out var entityValue);
            entity = entityValue;
            return exists;
        }
        
        public Dictionary<uint, Dictionary<Type, object>> Serialize()
        {
            var dictionary = new Dictionary<uint, Dictionary<Type, object>>();
            
            foreach (var (id, entity) in _idToEntity)
            {
                var types = Array.Empty<Type>();
                _world.GetComponentTypes(entity.EcsEntityId, ref types);
                
                var components = Array.Empty<object>();
                _world.GetComponents(entity.EcsEntityId, ref components);

                if (types.Any(x => x == typeof(SerializableEntity)))
                {
                    var componentsDictionary = new Dictionary<Type, object>();
                    for (var i = 0; i < types.Length; i++)
                        componentsDictionary[types[i]] = components[i];

                    dictionary[id] = componentsDictionary;
                }
            }

            return dictionary;
        }

        public void Deserialize(Dictionary<uint, Dictionary<Type, object>> dictionary)
        {
            var lastId = 0u;
            foreach (var (id, components) in dictionary)
            {
                var entity = _world.NewEntity();
                foreach (var (type, component) in components)
                    _world.GetPoolByType(type).SetRaw(entity, component);
                
                _idToEntity[id] = new Entity(id, entity, _world, this);
                lastId = (uint) Mathf.Max(lastId, id);
            }
            _lastId = lastId;
        }
    }
}