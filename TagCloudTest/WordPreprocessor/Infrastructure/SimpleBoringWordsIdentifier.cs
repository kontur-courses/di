using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Word;

namespace TagCloudTest.WordPreprocessor.Infrastructure
{
    public class SimpleBoringWordsIdentifier : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return word.Value == "boring";
        }
    }
}