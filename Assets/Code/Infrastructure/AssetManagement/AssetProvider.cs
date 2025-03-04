using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Infrastructure.AssetManagement
{
    public sealed class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, HandleInfo> _handles;

        public AssetProvider()
        {
            _handles = new Dictionary<string, HandleInfo>();
        }

        public Task<T> LoadAssetAsync<T>(AssetReference assetReference)
        {
            string assetKey = assetReference.AssetGUID;

            if (!_handles.TryGetValue(assetKey, out HandleInfo handleInfo))
            {
                AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(assetReference);
                handleInfo = new HandleInfo(handle);
                
                _handles[assetKey] = handleInfo;
            }

            handleInfo.ReferenceCount++;
            AsyncOperationHandle<T> operationHandle = handleInfo.Handle.Convert<T>();
            
            return operationHandle.Task;
        }
        
        public void Release(AssetReference assetReference)
        {
            string assetKey = assetReference.AssetGUID;

            if (!_handles.TryGetValue(assetKey, out HandleInfo handleInfo))
                return;
            
            if (0 < --handleInfo.ReferenceCount)
                return;
            
            _handles.Remove(assetKey);
            Addressables.Release(handleInfo.Handle);
        }
        
        public void ReleaseAll()
        {
            foreach (HandleInfo handleInfo in _handles.Values)
            {
                Addressables.Release(handleInfo.Handle);
            }

            _handles.Clear();
        }
        
        private sealed class HandleInfo
        {
            public AsyncOperationHandle Handle { get; }
            public int ReferenceCount { get; set; }
        
            public HandleInfo(AsyncOperationHandle handle)
            {
                Handle = handle;
                ReferenceCount = 0;
            }
        }
    }
}