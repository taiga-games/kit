using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TaigaGames.Kit.ECS
{
    public class EntityJsonSerialization
    {
        private readonly IEntityRegistry _entityRegistry;
        private readonly EcsWorld _world;
        
        public EntityJsonSerialization(IEntityRegistry entityRegistry, EcsWorld world)
        {
            _entityRegistry = entityRegistry;
            _world = world;
        }
        
        public string Serialize(IEntity entity)
        {
            var types = Array.Empty<Type>();
            _world.GetComponentTypes(entity.GetEcsEntityId(), ref types);
                
            var components = Array.Empty<object>();
            _world.GetComponents(entity.GetEcsEntityId(), ref components);

            var componentsDictionary = new Dictionary<Type, object>();
            if (types.Any(x => x == typeof(SerializableEntity)))
            {
                for (var i = 0; i < types.Length; i++)
                    componentsDictionary[types[i]] = components[i];
            }
            
            var entityJson = new EntityJson
            {
                EntityId = entity.GetId(),
                Components = componentsDictionary
            };

            return JsonConvert.SerializeObject(entityJson);
        }

        public IEntity Deserialize(string json)
        {
            var entityJson = JsonConvert.DeserializeObject<EntityJson>(json);

            var entity = _world.NewEntity();
            foreach (var (type, component) in entityJson.Components)
                _world.GetPoolByType(type).SetRaw(entity, component);

            return _entityRegistry.Register(entityJson.EntityId, entity);
        }
    }
}