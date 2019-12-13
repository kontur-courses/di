using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public static class ServiceSettings
    {
        private static readonly IDictionary<Type, Type> Services;

        static ServiceSettings()
        {
            Services = new Dictionary<Type, Type>();
        }

        internal static Type GetService<T>()
        {
            return Services[typeof(T)];
        }

        internal static void SetService<T, TService>() where TService : T
        {
            SetService<T>(typeof(TService));
        }

        internal static void SetService<T>(Type service)
        {
            var abstraction = typeof(T);
            if (abstraction.IsAssignableFrom(service))
                Services[abstraction] = service;
            else
                throw new ArgumentException($"{abstraction} isn't assignable from {service}");
        }
    }
}