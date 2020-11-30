using TagCloud.Core.ColoringAlgorithms;

namespace TagCloudUI.Infrastructure.Selectors
{
    public interface IColoringAlgorithmSelector
    {
        public bool TryGetAlgorithm(string name, out IColoringAlgorithm algorithm);
    }
}