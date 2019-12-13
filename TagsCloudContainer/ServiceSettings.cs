using System;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class ServiceSettings
    {
        private readonly IDictionary<Type, Type> services;

        public ServiceSettings()
        {
            services = new Dictionary<Type, Type>();
        }

        internal Type GetService<T>()
        {
            return services[typeof(T)];
        }

        internal void SetService<T, TService>() where TService : T
        {
            SetService<T>(typeof(TService));
        }

        internal void SetService<T>(Type service)
        {
            var abstraction = typeof(T);
            if (abstraction.IsAssignableFrom(service))
                services[abstraction] = service;
            else
                throw new ArgumentException($"{abstraction} isn't assignable from {service}");
        }
    }
}