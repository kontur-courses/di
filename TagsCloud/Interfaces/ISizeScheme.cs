using TagsCloudVisualization.Layouter;

namespace TagsCloudVisualization.Interfaces
{
    public interface ISizeScheme
    {
        Size GetSize(FrequentedFontedWord element);
    }
}