using TaigaGames.Kit.ECS;
using Zenject;

namespace TaigaGames
{
    public class EcsFeatureRegistrator<T> : IInitializable, IEcsFeatureRegistrator where T : IEcsFeature
    {
        [Inject] private readonly DiContainer _diContainer;
        [Inject] private readonly EcsRunnerForZenject _ecsRunnerForZenject;
        
        public void Initialize()
        {
            _ecsRunnerForZenject.AddFeature(_diContainer.Instantiate<T>());
        }
    }

    public interface IEcsFeatureRegistrator
    {
    }
}