using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Word;

namespace TagCloudTest.WordPreprocessor.Infrastructure
{
    public class SimpleVeryBoringWordsIdentifier : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return word.Value == "very-boring";
        }
    }
}