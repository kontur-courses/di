using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.ColoringAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public class ColoringAlgorithmSelector : IColoringAlgorithmSelector
    {
        private readonly Dictionary<string, IColoringAlgorithm> nameToAlgorithm;

        public ColoringAlgorithmSelector(IEnumerable<IColoringAlgorithm> algorithms)
        {
            nameToAlgorithm = algorithms.ToDictionary(al => al.Name);
        }

        public bool TryGetAlgorithm(string name, out IColoringAlgorithm algorithm)
        {
            return nameToAlgorithm.TryGetValue(name, out algorithm);
        }
    }
}