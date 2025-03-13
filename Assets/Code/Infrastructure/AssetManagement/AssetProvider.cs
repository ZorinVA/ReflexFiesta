using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Code.Infrastructure.AssetManagement
{
    public sealed class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, HandleDetails> _handles;

        public AssetProvider()
        {
            _handles = new Dictionary<string, HandleDetails>();
        }

        public UniTask<T> LoadAssetAsync<T>(AssetReference assetReference)
        {
            string assetKey = assetReference.AssetGUID;

            if (!_handles.TryGetValue(assetKey, out HandleDetails handleInfo))
            {
                AsyncOperationHandle handle = Addressables.LoadAssetAsync<T>(assetReference);
                handleInfo = new HandleDetails(handle);
                
                _handles[assetKey] = handleInfo;
            }

            handleInfo.ReferenceCount++;
            AsyncOperationHandle<T> operationHandle = handleInfo.Handle.Convert<T>();
            
            return operationHandle.Task.AsUniTask();
        }
        
        public void Release(AssetReference assetReference)
        {
            string assetKey = assetReference.AssetGUID;

            if (!_handles.TryGetValue(assetKey, out HandleDetails handleInfo))
                return;
            
            if (0 < --handleInfo.ReferenceCount)
                return;
            
            _handles.Remove(assetKey);
            Addressables.Release(handleInfo.Handle);
        }
        
        public void ReleaseAll()
        {
            foreach (HandleDetails handleInfo in _handles.Values)
            {
                Addressables.Release(handleInfo.Handle);
            }

            _handles.Clear();
        }
        
        private sealed class HandleDetails
        {
            public AsyncOperationHandle Handle { get; }
            public int ReferenceCount { get; set; }
        
            public HandleDetails(AsyncOperationHandle handle)
            {
                Handle = handle;
                ReferenceCount = 0;
            }
        }
    }
}