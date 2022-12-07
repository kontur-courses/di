using TagsCloudVisualisation.App.WordsPreprocessor.BoringWords;
using TagsCloudVisualisation.App.WordsPreprocessor.Word;

namespace TagsCloudVisualisation.Tests.WordPreprocessor.Infrastructure
{
    public class SimpleBoringWordsIdentifier : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return word.Value == "boring";
        }
    }
}