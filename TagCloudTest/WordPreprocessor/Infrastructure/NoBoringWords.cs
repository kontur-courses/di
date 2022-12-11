using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloudTest.WordPreprocessor.Infrastructure;

public class NoBoringWords : IBoringWords
{
    public bool IsBoring(IWord word)
    {
        return false;
    }
}