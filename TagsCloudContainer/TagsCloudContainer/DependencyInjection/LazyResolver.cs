using System;
using Microsoft.Extensions.DependencyInjection;

namespace TagsCloudContainer.DependencyInjection
{
    public class LazyResolver<T> : Lazy<T> where T : class
    {
        public LazyResolver(IServiceProvider provider)
            : base(provider.GetRequiredService<T>) { }
    }
}