using UnityEngine;

namespace TaigaGames.Kit.ECS
{
    public abstract class ConvertComponent<T> : MonoBehaviour, IConvertableToEntity where T : struct
    {
        public T Value;
        
        public void ConvertToEntity(EcsWorld world, int entity)
        {
            var pool = world.GetPool<T>();
            pool.Add(entity) = Value;
        }
    }
}