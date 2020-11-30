using System.Collections.Generic;
using System.Linq;
using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public class LayoutAlgorithmSelector : ILayoutAlgorithmSelector
    {
        private readonly Dictionary<string, ILayoutAlgorithm> nameToAlgorithm;

        public LayoutAlgorithmSelector(IEnumerable<ILayoutAlgorithm> algorithms)
        {
            nameToAlgorithm = algorithms.ToDictionary(al => al.Name);
        }

        public bool TryGetAlgorithm(string name, out ILayoutAlgorithm algorithm)
        {
            return nameToAlgorithm.TryGetValue(name, out algorithm);
        }
    }
}