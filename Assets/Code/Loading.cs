using Code.Infrastructure.Logging;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;

public sealed class Loading : MonoBehaviour
{
    private ILog _log;

    [Inject]
    public void Construct(ILog log)
    {
        _log = log;
    }
    
    private void Start()
    {
        Addressables.LoadSceneAsync("LoadingScene", activateOnLoad: false).Completed += handle =>
        {
            handle.Result.ActivateAsync();
        };
        
        _log.Write("Mc");
    }
}