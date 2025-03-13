using System;
using Reflex.Core;

namespace Code.Infrastructure.ObjectProduction
{
    public sealed class ObjectFactory : IObjectFactory
    {
        private readonly Container _container;

        public ObjectFactory(Container container)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
        }

        public T Construct<T>() => 
            _container.Construct<T>();
    }
}