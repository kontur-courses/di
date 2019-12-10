using TagsCloudContainer.TokensAndSettings;

namespace TagsCloudContainer.WordFilters
{
    public interface IWordFilter
    {
        bool IsCorrect(ProcessedWord word);
    }
}
