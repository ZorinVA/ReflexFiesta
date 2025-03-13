using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        UniTask<T> LoadAssetAsync<T>(AssetReference assetReference);
        void Release(AssetReference assetReference);
        void ReleaseAll();
    }
}