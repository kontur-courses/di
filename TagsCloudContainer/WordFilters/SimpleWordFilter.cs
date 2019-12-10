using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.WordFilters
{
    public class SimpleWordFilter : IWordFilter
    {
        public bool IsCorrect(ProcessedWord word)
        {
            return true;
        }
    }
}
