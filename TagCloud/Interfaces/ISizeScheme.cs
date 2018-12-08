using TagCloud.IntermediateClasses;
using TagCloud.Layouter;

namespace TagCloud.Interfaces
{
    public interface ISizeScheme
    {
        Size GetSize(FrequentedWord word);
    }
}