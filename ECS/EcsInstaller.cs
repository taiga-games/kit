using TaigaGames.Kit.ECS;
using TaigaGames.Kit.Signals;
using UnityEngine;
using Zenject;

namespace TaigaGames.ECS
{
    public class EcsInstaller : MonoInstaller
    {
        [SerializeField] private SceneContext _sceneContext;

        private void Reset()
        {
            _sceneContext = FindObjectOfType<SceneContext>();
        }

        public override void InstallBindings()
        {
            Container.BindService<EcsWorld>();
            Container.BindService<EcsRunnerForZenject>();
            
            Container.BindService<EntityRegistry>();
            Container.BindService<SignalBus>();
            Container.BindService<KeySignalBus<Entity>>();
            
            _sceneContext.PostResolve += OnSceneContextPostResolve;
        }   

        private void OnSceneContextPostResolve()
        {
            _sceneContext.PostResolve -= OnSceneContextPostResolve;
            var world = Container.Resolve<EcsWorld>();
            foreach (var contract in Container.AllContracts)
            {
                if (contract.Type.ContainsGenericParameters)
                    continue;
                
                var objects = Container.ResolveAll(contract.Type);
                foreach (var obj in objects) 
                    EcsInjectionUtils.InjectEcs(obj, world);       
            }
        }
    }
}