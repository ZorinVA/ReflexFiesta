using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        Task<T> LoadAssetAsync<T>(AssetReference assetReference);
        void Release(AssetReference assetReference);
        void ReleaseAll();
    }
}