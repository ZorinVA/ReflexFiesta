using Cysharp.Threading.Tasks;
using Reflex.Core;
using UnityEngine;

namespace Code.Infrastructure.ObjectProduction
{
    public sealed class GameObjectFactory : IGameObjectFactory
    {
        private readonly Container _container;

        public GameObjectFactory(Container container)
        {
            _container = container ?? throw new System.ArgumentNullException(nameof(container));
        }

        public GameObject Instantiate(GameObject origin) => 
            Object
                .Instantiate(origin)
                .InjectRecursive(_container);

        public T Instantiate<T>(T origin) where T : Component => 
            Object
                .Instantiate(origin)
                .InjectRecursive(_container);
        
        public async UniTask<GameObject> InstantiateAsync(GameObject origin, Transform parent = null)
        {
            GameObject[] instances = await Object.InstantiateAsync(origin, parent);
            GameObject[] injectedInstances = InjectAll(instances);

            return injectedInstances[0];
        }
        
        public async UniTask<T> InstantiateAsync<T>(T origin, Transform parent = null) where T : Component
        {
            T[] instances = await Object.InstantiateAsync(origin, parent);
            T[] injectedInstances = InjectAll(instances);

            return injectedInstances[0];
        }

        private GameObject[] InjectAll(GameObject[] instances)
        {
            for (int i = 0; i < instances.Length; i++)
            {
                instances[i].InjectRecursive(_container);
            }

            return instances;
        }
        
        private T[] InjectAll<T>(T[] instances) where T : Component
        {
            for (int i = 0; i < instances.Length; i++)
            {
                instances[i].InjectRecursive(_container);
            }

            return instances;
        }
    }
}