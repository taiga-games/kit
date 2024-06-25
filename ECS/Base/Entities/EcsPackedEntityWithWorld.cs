namespace TaigaGames.Kit.ECS
{
    public struct EcsPackedEntityWithWorld
    {
        public int Id;
        public int Gen;
        public EcsWorld World;

        public override int GetHashCode()
        {
            unchecked
            {
                return ((23 * 31 + Id) * 31 + Gen) * 31 + (World?.GetHashCode() ?? 0);
            }
        }
#if DEBUG
        internal object[] DebugComponentsView
        {
            get
            {
                object[] list = null;
                if (World != null && World.IsAlive() && World.IsEntityAliveInternal(Id) && World.GetEntityGen(Id) == Gen)
                {
                    World.GetComponents(Id, ref list);
                }

                return list;
            }
        }

        internal int DebugComponentsCount
        {
            get
            {
                if (World != null && World.IsAlive() && World.IsEntityAliveInternal(Id) && World.GetEntityGen(Id) == Gen)
                {
                    return World.GetComponentsCount(Id);
                }

                return 0;
            }
        }

        public override string ToString()
        {
            if (Id == 0 && Gen == 0)
            {
                return "Entity-Null";
            }

            if (World == null || !World.IsAlive() || !World.IsEntityAliveInternal(Id) || World.GetEntityGen(Id) != Gen)
            {
                return "Entity-NonAlive";
            }

            System.Type[] types = null;
            var count = World.GetComponentTypes(Id, ref types);
            System.Text.StringBuilder sb = null;
            if (count > 0)
            {
                sb = new System.Text.StringBuilder(512);
                for (var i = 0; i < count; i++)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(",");
                    }

                    sb.Append(types[i].Name);
                }
            }

            return $"Entity-{Id}:{Gen} [{sb}]";
        }
#endif
    }
}