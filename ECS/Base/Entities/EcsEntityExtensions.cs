using System.Runtime.CompilerServices;

namespace TaigaGames.Kit.ECS
{
    public static class EcsEntityExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EcsPackedEntity PackEntity(this EcsWorld world, int entity)
        {
            EcsPackedEntity packed;
            packed.Id = entity;
            packed.Gen = world.GetEntityGen(entity);
            return packed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unpack(this in EcsPackedEntity packed, EcsWorld world, out int entity)
        {
            entity = packed.Id;
            return
                world != null
                && world.IsAlive()
                && world.IsEntityAliveInternal(packed.Id)
                && world.GetEntityGen(packed.Id) == packed.Gen;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo(this in EcsPackedEntity a, in EcsPackedEntity b)
        {
            return a.Id == b.Id && a.Gen == b.Gen;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EcsPackedEntityWithWorld PackEntityWithWorld(this EcsWorld world, int entity)
        {
            EcsPackedEntityWithWorld packedEntity;
            packedEntity.World = world;
            packedEntity.Id = entity;
            packedEntity.Gen = world.GetEntityGen(entity);
            return packedEntity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Unpack(this in EcsPackedEntityWithWorld packedEntity, out EcsWorld world, out int entity)
        {
            world = packedEntity.World;
            entity = packedEntity.Id;
            return
                world != null
                && world.IsAlive()
                && world.IsEntityAliveInternal(packedEntity.Id)
                && world.GetEntityGen(packedEntity.Id) == packedEntity.Gen;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool EqualsTo(this in EcsPackedEntityWithWorld a, in EcsPackedEntityWithWorld b)
        {
            return a.Id == b.Id && a.Gen == b.Gen && a.World == b.World;
        }
    }
}