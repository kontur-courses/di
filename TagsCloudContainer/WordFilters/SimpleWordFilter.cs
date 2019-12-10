namespace TagsCloudContainer.WordFilters
{
    public class SimpleWordFilter : IWordFilter
    {
        public bool IsCorrect(string word)
        {
            return true;
        }
    }
}
