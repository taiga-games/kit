using System;
using System.Runtime.CompilerServices;

#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace TaigaGames.Kit.ECS
{
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public sealed class EcsFilter
    {
        private readonly EcsWorld _world;
        private readonly EcsWorld.Mask _mask;
        private int[] _denseEntities;
        private int _entitiesCount;
        internal int[] SparseEntities;
        private int _lockCount;
        private DelayedOp[] _delayedOps;
        private int _delayedOpsCount;

        internal EcsFilter(EcsWorld world, EcsWorld.Mask mask, int denseCapacity, int sparseCapacity)
        {
            _world = world;
            _mask = mask;
            _denseEntities = new int[denseCapacity];
            SparseEntities = new int[sparseCapacity];
            _entitiesCount = 0;
            _delayedOps = new DelayedOp[512];
            _delayedOpsCount = 0;
            _lockCount = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EcsWorld GetWorld()
        {
            return _world;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetEntitiesCount()
        {
            return _entitiesCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int[] GetRawEntities()
        {
            return _denseEntities;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int[] GetSparseIndex()
        {
            return SparseEntities;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Enumerator GetEnumerator()
        {
            _lockCount++;
            return new Enumerator(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void ResizeSparseIndex(int capacity)
        {
            Array.Resize(ref SparseEntities, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal EcsWorld.Mask GetMask()
        {
            return _mask;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void AddEntity(int entity)
        {
            if (AddDelayedOp(true, entity))
            {
                return;
            }

            if (_entitiesCount == _denseEntities.Length)
            {
                Array.Resize(ref _denseEntities, _entitiesCount << 1);
            }

            _denseEntities[_entitiesCount++] = entity;
            SparseEntities[entity] = _entitiesCount;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void RemoveEntity(int entity)
        {
            if (AddDelayedOp(false, entity))
            {
                return;
            }

            var idx = SparseEntities[entity] - 1;
            SparseEntities[entity] = 0;
            _entitiesCount--;
            if (idx < _entitiesCount)
            {
                _denseEntities[idx] = _denseEntities[_entitiesCount];
                SparseEntities[_denseEntities[idx]] = idx + 1;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool AddDelayedOp(bool added, int entity)
        {
            if (_lockCount <= 0)
            {
                return false;
            }

            if (_delayedOpsCount == _delayedOps.Length)
            {
                Array.Resize(ref _delayedOps, _delayedOpsCount << 1);
            }

            ref var op = ref _delayedOps[_delayedOpsCount++];
            op.Added = added;
            op.Entity = entity;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Unlock()
        {
#if DEBUG && !TG_ECS_NO_SANITIZE_CHECKS
            if (_lockCount <= 0)
                throw new Exception($"Invalid lock-unlock balance for \"{GetType().Name}\".");
#endif
            _lockCount--;
            if (_lockCount == 0 && _delayedOpsCount > 0)
            {
                for (int i = 0, iMax = _delayedOpsCount; i < iMax; i++)
                {
                    ref var op = ref _delayedOps[i];
                    if (op.Added)
                        AddEntity(op.Entity);
                    else
                        RemoveEntity(op.Entity);
                }

                _delayedOpsCount = 0;
            }
        }

        public struct Enumerator : IDisposable
        {
            private readonly EcsFilter _filter;
            private readonly int[] _entities;
            private readonly int _count;
            private int _idx;

            public Enumerator(EcsFilter filter)
            {
                _filter = filter;
                _entities = filter._denseEntities;
                _count = filter._entitiesCount;
                _idx = -1;
            }

            public int Current
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _entities[_idx];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public bool MoveNext()
            {
                return ++_idx < _count;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void Dispose()
            {
                _filter.Unlock();
            }
        }

        private struct DelayedOp
        {
            public bool Added;
            public int Entity;
        }
    }
}