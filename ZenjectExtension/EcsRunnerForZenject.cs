using System;
using TaigaGames.Kit.ECS;
using Zenject;

namespace TaigaGames
{
    public class EcsRunnerForZenject : EcsRunner, ITickable, ILateTickable, IFixedTickable, IDisposable
    {
        private bool _started;
        
        public EcsRunnerForZenject(EcsWorld world) : base(world)
        {
        }

        public void Start()
        {
            Init();
            _started = true;
        }

        public void Tick()
        {
            if (!_started) return;
            Update();
        }

        public void LateTick()
        {
            if (!_started) return;
            LateUpdate();
        }

        public void FixedTick()
        {
            if (!_started) return;
            FixedUpdate();
        }

        public void Dispose()
        {
            if (!_started) return;
            Destroy();
        }
    }
}