using System.Collections.Generic;

namespace TaigaGames.Kit.ECS
{
    public abstract class EcsRunner
    {
        protected readonly List<EcsSystemsGroup> Systems = new();
        protected readonly EcsWorld World;

        public EcsRunner(EcsWorld world)
        {
            World = world;
        }

        public virtual EcsRunner AddFeature(IEcsFeature feature)
        {
            var updateSystems = new EcsSystems(World);
            var lateUpdateSystems = new EcsSystems(World);
            var fixedUpdateSystems = new EcsSystems(World);
                
            if (feature is IEcsUpdateFeature updateFeature)
                updateFeature.SetupUpdateSystems(updateSystems);
                
            if (feature is IEcsLateUpdateFeature lateUpdateFeature)
                lateUpdateFeature.SetupLateUpdateSystems(lateUpdateSystems);
                
            if (feature is IEcsFixedUpdateFeature fixedUpdateFeature)
                fixedUpdateFeature.SetupFixedUpdateSystems(fixedUpdateSystems);
                
            var systemsGroup = new EcsSystemsGroup(updateSystems, lateUpdateSystems, fixedUpdateSystems);
            Inject(systemsGroup);
            Systems.Add(systemsGroup);

            return this;
        }
        
        protected virtual void Init()
        {
            foreach (var systems in Systems)
                systems.Init();
        }

        protected virtual void Update()
        {
            foreach (var systems in Systems)
                systems.GetUpdateSystems().Run();
        }

        protected virtual void LateUpdate()
        {
            foreach (var systems in Systems)
                systems.GetLateUpdateSystems().Run();
        }

        protected virtual void FixedUpdate()
        {
            foreach (var systems in Systems)
                systems.GetFixedUpdateSystems().Run();
        }

        protected virtual void Destroy()
        {
            foreach (var systems in Systems)
                systems.Destroy();
        }

        protected virtual void Inject(EcsSystemsGroup systemsGroup)
        {
            foreach (var system in systemsGroup.GetAllSystems()) 
                EcsInjectionUtils.InjectEcs(system, World);
        }
    }
}