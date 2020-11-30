using TagCloud.Core.LayoutAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public interface ILayoutAlgorithmSelector
    {
        public bool TryGetAlgorithm(string name, out ILayoutAlgorithm algorithm);
    }
}