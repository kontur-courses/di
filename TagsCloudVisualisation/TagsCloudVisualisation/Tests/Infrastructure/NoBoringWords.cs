using TagsCloudVisualisation.WordsPreprocessor.BoringWords;

namespace TagsCloudVisualisation.Tests.Infrastructure
{
    public class NoBoringWords : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return false;
        }
    }
}