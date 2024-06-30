using System;
using System.Collections.Generic;

namespace TaigaGames.Kit.ECS
{
    [Serializable]
    public struct EntityJson
    {
        public uint EntityId;
        public Dictionary<Type, object> Components;
    }
}