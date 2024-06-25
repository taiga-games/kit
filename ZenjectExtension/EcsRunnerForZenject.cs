using System;
using System.Collections.Generic;
using TaigaGames.Kit.ECS;
using Zenject;

namespace TaigaGames
{
    public class EcsRunnerForZenject : EcsRunner, IInitializable, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private readonly DiContainer _diContainer;
        private readonly Queue<Type> _features = new Queue<Type>();
        
        public EcsRunnerForZenject(EcsWorld world, DiContainer diContainer) : base(world)
        {
            _diContainer = diContainer;
        }

        public void Initialize()
        {
            while (_features.TryDequeue(out var featureType))
                AddFeature((IEcsFeature)_diContainer.Instantiate(featureType));

            Init();
        }

        public void Tick()
        {
            Update();
        }

        public void LateTick()
        {
            LateUpdate();
        }

        public void FixedTick()
        {
            FixedUpdate();
        }

        public void Dispose()
        {
            Destroy();
        }

        public EcsRunner AddFeature<T>() where T : IEcsFeature
        {
            _features.Enqueue(typeof(T));
            return this;
        }
    }
}