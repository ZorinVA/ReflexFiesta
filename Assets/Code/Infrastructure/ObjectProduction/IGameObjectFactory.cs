using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Infrastructure.ObjectProduction
{
    public interface IGameObjectFactory
    {
        GameObject Instantiate(GameObject origin);
        T Instantiate<T>(T origin) where T : Component;
        UniTask<GameObject> InstantiateAsync(GameObject origin, Transform parent = null);
        UniTask<T> InstantiateAsync<T>(T origin, Transform parent = null) where T : Component;
    }
}