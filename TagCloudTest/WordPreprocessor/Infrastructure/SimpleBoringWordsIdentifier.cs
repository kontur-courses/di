using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloudTest.WordPreprocessor.Infrastructure;

public class SimpleBoringWordsIdentifier : IBoringWords
{
    public bool IsBoring(IWord word)
    {
        return word.Value == "boring";
    }
}