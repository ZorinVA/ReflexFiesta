using System.Collections.Generic;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Logging;
using Code.Infrastructure.ObjectProduction;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Code
{
    public sealed class LoadingInjectTester : MonoBehaviour
    {
        [SerializeField] private AssetReference _prototypeReference;
        
        private IAssetProvider _assetProvider;
        private IObjectFactory _factory;
        private ILog _log;
        private IEnumerable<string> _strings;

        [Inject]
        private void Construct(IAssetProvider assetProvider, IObjectFactory factory, ILog log, IEnumerable<string> strings)
        {
            _assetProvider = assetProvider;
            _factory = factory;
            _log = log;
            _strings = strings;
        }
        
        private async void Start()
        {
            _log.Write(string.Join(" ", _strings));

            GameObject prototypeAsset = await _assetProvider.LoadAssetAsync<GameObject>(_prototypeReference);
            GameObject instance = _factory.Instantiate(prototypeAsset);
            
            _assetProvider.Release(_prototypeReference);
        }
    }
}