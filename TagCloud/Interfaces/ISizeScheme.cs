using TagCloud.Layouter;

namespace TagCloud.Interfaces
{
    public interface ISizeScheme
    {
        Size GetSize(FrequentedFontedWord element);
    }
}