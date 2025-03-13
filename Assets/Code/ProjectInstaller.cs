using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Logging;
using Code.Infrastructure.ObjectProduction;
using Reflex.Core;
using UnityEngine;

namespace Code
{
    public sealed class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            containerBuilder
                .AddSingleton("Hello")
                .AddSingleton(concrete: typeof(ObjectFactory), contracts: typeof(IObjectFactory))
                .AddSingleton(concrete: typeof(GameObjectFactory), contracts: typeof(IGameObjectFactory))
                .AddSingleton(concrete: typeof(UnityLog), contracts: typeof(ILog))
                .AddSingleton(concrete: typeof(AssetProvider), contracts: typeof(IAssetProvider));
        }
    }
}