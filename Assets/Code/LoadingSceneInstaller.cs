using Reflex.Core;
using UnityEngine;

namespace Code
{
    public sealed class LoadingSceneInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton("World")
                .Build();
        }
    }
}