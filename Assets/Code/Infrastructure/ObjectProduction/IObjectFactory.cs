using UnityEngine;

namespace Code.Infrastructure.ObjectProduction
{
    public interface IObjectFactory
    {
        T Construct<T>();
        GameObject Instantiate(GameObject prototype);
        T Instantiate<T>(T prototype) where T : Component;
    }
}