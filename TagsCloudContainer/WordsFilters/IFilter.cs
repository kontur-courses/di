namespace TagsCloudContainer.WordsFilters
{
    public interface IFilter<in TValue>
    {
        bool IsCorrect(TValue value);
    }
}