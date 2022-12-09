using CircularCloudLayouter.Domain;

namespace TagCloudCreator.Interfaces;

public interface IWordSizeCalculator
{
    ImmutableSize GetSizeFor(string word, int fontSize);
}