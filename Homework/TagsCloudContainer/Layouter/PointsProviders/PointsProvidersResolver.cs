using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Layouter.PointsProviders
{
    public class PointsProvidersResolver : IResolver<LayoutAlrogorithm, IPointsProvider>
    {
        private readonly Dictionary<LayoutAlrogorithm, IPointsProvider> resolver;


        public PointsProvidersResolver(IPointsProvider[] pointsProvider)
        {
            resolver = pointsProvider.ToDictionary(x => x.AlghorithmName);
        }

        public IPointsProvider Get(LayoutAlrogorithm algorithm)
        {
            if (resolver.ContainsKey(algorithm))
                return resolver[algorithm];
            throw new ArgumentException($"{algorithm} does not exist");
        }
    }
}