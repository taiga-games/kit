using System;

namespace TaigaGames.Kit.ECS
{
    public delegate void EntityComponentAdded(IEntity entity, Type type);
    public delegate void EntityComponentChanged(IEntity entity, Type type);
    public delegate void EntityComponentRemoved(IEntity entity, Type type);
}