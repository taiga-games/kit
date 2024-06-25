using TaigaGames.Kit.ECS;
using TaigaGames.Kit.Signals;
using UnityEngine;
using Zenject;

namespace TaigaGames.ECS
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindService<EcsWorld>();
            Container.BindService<EcsRunnerForZenject>();
            
            Container.BindService<EntityRegistry>();
            Container.BindService<SignalBus>();
            Container.BindService<KeySignalBus<Entity>>();
        }
    }
}