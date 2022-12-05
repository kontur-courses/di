using TagsCloudVisualisation.WordsPreprocessor.BoringWords;

namespace TagsCloudVisualisation.Tests.Infrastructure
{
    public class SimpleVeryBoringWordsIdentifier : IBoringWords
    {
        public bool IsBoring(Word word)
        {
            return word.Value == "very-boring";
        }
    }
}