using TagsCloudVisualisation.App.WordsPreprocessor.BoringWords;
using TagsCloudVisualisation.App.WordsPreprocessor.Word;

namespace TagsCloudVisualisation.Tests.WordPreprocessor.Infrastructure
{
    public class NoBoringWords : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return false;
        }
    }
}