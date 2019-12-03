namespace TextConfiguration.WordFilters
{
    public interface IWordFilter
    {
        bool ShouldFilter(string word);
    }
}
