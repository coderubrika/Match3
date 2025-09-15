using System;
using System.Collections.Generic;

namespace Test3
{
    public sealed class ServiceLocator
    {
        private ServiceLocator() {}

        private static ServiceLocator instance;

        private readonly Dictionary<Type, object> instances = new();
        
        public static ServiceLocator Instance => instance ??= new ServiceLocator();

        
        
        public void BindInstance<T>(T tInstance)
            where T : class
        {
            instances.TryAdd(typeof(T), tInstance);
        }

        public T Get<T>()
            where T : class
        {
            return instances.TryGetValue(typeof(T), out object value) ? value as T : null;
        }
    }
}