using System.Linq;
using TaigaGames.Kit.ECS;

namespace TaigaGames.ECS
{
    public static class EcsFilterEx
    {
        public static bool Match(this EcsFilter filter, int entity)
        {
            return filter.GetRawEntities().Contains(entity);
        }

        public static bool Match(this EcsFilter filter, IEntity entity)
        {
            return filter.GetRawEntities().Contains(entity.GetEcsEntityId());
        }
    }
}