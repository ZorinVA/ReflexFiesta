using Reflex.Core;
using Reflex.Injectors;
using UnityEngine;

namespace Code.Infrastructure.ObjectProduction
{
    public static class UnityInjectExtensions
    {
        public static GameObject InjectRecursive(this GameObject instance, Container container)
        {
            GameObjectInjector.InjectRecursive(instance, container);
            return instance;
        }

        public static T InjectRecursive<T>(this T instance, Container container) where T : Component
        {
            GameObjectInjector.InjectRecursive(instance.gameObject, container);
            return instance;
        }
    }
}