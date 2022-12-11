using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Words;

namespace TagCloudTest.WordPreprocessor.Infrastructure;

public class SimpleVeryBoringWordsIdentifier : IBoringWords
{
    public bool IsBoring(IWord word)
    {
        return word.Value == "very-boring";
    }
}