using Reflex.Core;
using Reflex.Extensions;
using Reflex.Injectors;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.ObjectProduction
{
    public sealed class ObjectFactory : IObjectFactory
    {
        public T Construct<T>()
        {
            Container container = CurrentContainer();
            T instance = container.Construct<T>();
            
            return instance;
        }

        public GameObject Instantiate(GameObject prototype)
        {
            Container container = CurrentContainer();
            GameObject instance = Object.Instantiate(prototype);

            GameObjectInjector.InjectRecursive(instance, container);
            
            return instance;
        }

        public T Instantiate<T>(T prototype) where T : Component
        {
            Container container = CurrentContainer();
            T instance = Object.Instantiate(prototype);

            GameObjectInjector.InjectRecursive(instance.gameObject, container);
            
            return instance;
        }

        private Container CurrentContainer()
        {
            Scene scene = SceneManager.GetActiveScene();
            Container container = scene.GetSceneContainer();

            return container;
        }
    }
}