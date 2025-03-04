using UnityEngine;

namespace Code.Infrastructure.Logging
{
    public sealed class UnityLog : ILog
    {
        public void Write(string message) => 
            Debug.Log(message);
        
        public void WriteWarning(string message) => 
            Debug.LogWarning(message);
        
        public void WriteError(string message) => 
            Debug.LogError(message);
    }
}