using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.Visualizer;

namespace TagsCloudVisualization.Interfaces
{
    public interface ISizeScheme
    {
        Size GetSize(FrequentedWord element);
    }
}