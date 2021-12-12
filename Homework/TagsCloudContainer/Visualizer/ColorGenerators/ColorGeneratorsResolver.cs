using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Visualizer.ColorGenerators
{
    public class ColorGeneratorsResolver : IResolver<PalleteType, IColorGenerator>
    {
        private readonly Dictionary<PalleteType, IColorGenerator> resolver;

        public ColorGeneratorsResolver(IColorGenerator[] colorGenerators)
        {
            resolver = colorGenerators.ToDictionary(x => x.PalleteType);
        }

        public IColorGenerator Get(PalleteType palleteType)
        {
            if (resolver.ContainsKey(palleteType))
                return resolver[palleteType];
            throw new ArgumentException($"{palleteType} not found");
        }
    }
}