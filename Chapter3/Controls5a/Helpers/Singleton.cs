using System;
using System.Collections.Concurrent;

namespace Controls5a.Helpers
{
    internal static class Singleton<T>
        where T : new()
    {
        private static ConcurrentDictionary<Type, T> _instances = new ConcurrentDictionary<Type, T>();

        public static T Instance
        {
            get
            {
#pragma warning disable IDE0025 // Use expression body for properties
                return _instances.GetOrAdd(typeof(T), (t) => new T());
#pragma warning restore IDE0025 // Use expression body for properties
            }
        }
    }
}
