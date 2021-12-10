using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.DependencyInjection
{
    public class ServiceResolver<TType, TService> : IServiceResolver<TType, TService>
        where TService : IService<TType> where TType : notnull
    {
        private readonly Dictionary<TType, TService> serviceResolver;

        public ServiceResolver(IEnumerable<TService> services)
        {
            serviceResolver = services.ToDictionary(s => s.Type);
        }

        public TService GetService(TType type) => serviceResolver[type];
    }
}