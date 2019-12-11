namespace TextConfiguration.WordFilters
{
    public interface IWordFilter
    {
        bool ShouldExclude(string word);
    }
}
