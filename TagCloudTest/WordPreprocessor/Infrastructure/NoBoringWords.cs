using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.BoringWords;
using TagCloud.App.WordPreprocessorDriver.WordsPreprocessor.Word;

namespace TagCloudTest.WordPreprocessor.Infrastructure
{
    public class NoBoringWords : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return false;
        }
    }
}