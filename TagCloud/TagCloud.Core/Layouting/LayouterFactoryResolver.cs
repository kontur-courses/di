using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Core.Layouting
{
    public class LayouterFactoryResolver : ILayouterFactoryResolver
    {
        private readonly Dictionary<TagCloudLayouterType, ILayouterFactory> layouterFactories;

        public LayouterFactoryResolver(IEnumerable<ILayouterFactory> layouters)
        {
            layouterFactories = layouters.ToDictionary(x => x.Type);
        }

        public ILayouterFactory Get(TagCloudLayouterType type) => layouterFactories[type];
    }
}