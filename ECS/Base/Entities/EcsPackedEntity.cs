#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace TaigaGames.Kit.ECS
{
    public struct EcsPackedEntity
    {
        public int Id;
        public int Gen;

        public override int GetHashCode()
        {
            unchecked
            {
                return (23 * 31 + Id) * 31 + Gen;
            }
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
}