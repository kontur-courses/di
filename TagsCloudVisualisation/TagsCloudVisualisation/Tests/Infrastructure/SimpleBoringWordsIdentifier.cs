using TagsCloudVisualisation.WordsPreprocessor.BoringWords;

namespace TagsCloudVisualisation.Tests.Infrastructure
{
    public class SimpleBoringWordsIdentifier : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return word.Value == "boring";
        }
    }
}