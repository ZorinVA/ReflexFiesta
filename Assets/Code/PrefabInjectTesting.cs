using Code.Infrastructure.Logging;
using Reflex.Attributes;
using UnityEngine;

namespace Code
{
    public sealed class PrefabInjectTesting : MonoBehaviour
    {
        private ILog _log;

        [Inject]
        public void Construct(ILog log)
        {
            _log = log;
        }

        private void Start()
        {
            _log.Write("DUCK");
        }

        private void OnDestroy()
        {
            _log.Write("OnDestroy");
        }
    }
}