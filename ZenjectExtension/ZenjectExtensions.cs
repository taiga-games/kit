using TaigaGames.Kit.ECS;
using Zenject;

namespace TaigaGames
{
    public static class ZenjectExtensions
    {
        public static void BindObjectOnScene<T>(this DiContainer container, T objectOnScene) where T : UnityEngine.Object
        {
            container.BindInstance(objectOnScene).AsSingle().NonLazy();
        }
        
        public static void BindScenario<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
        
        public static void BindService<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
        
        public static void BindTask<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
        
        public static void BindHandler<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
        
        public static void BindScriptableObject<T>(this DiContainer container, T scriptableObject) where T : UnityEngine.ScriptableObject
        {
            container.BindInstance(scriptableObject).AsSingle().NonLazy();
        }
        
        public static void BindViaSpawnNewPrefab<T>(this DiContainer container, T prefab) where T : UnityEngine.Behaviour
        {
            container.BindInterfacesAndSelfTo<T>().FromComponentsInNewPrefab(prefab).AsSingle().NonLazy();
        }
        
        public static void BindPrefab<T>(this DiContainer container, T prefab) where T : UnityEngine.MonoBehaviour
        {
            container.BindInstance(prefab).AsSingle().NonLazy();
        }
        
        public static void BindEcsFeature<T>(this DiContainer container) where T : IEcsFeatureRegistrator
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle().NonLazy();
        }
        
        public static void BindScreen<TScreenView, TScreenController>(this DiContainer container, TScreenView prefab)
        {
            container.BindInstance(prefab).AsSingle().NonLazy();
            container.BindInterfacesAndSelfTo<TScreenController>().AsSingle().NonLazy();
        }
    }
}