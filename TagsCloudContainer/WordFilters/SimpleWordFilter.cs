namespace TagsCloudContainer.WordFilters
{
    class SimpleWordFilter : IWordFilter
    {
        public bool IsCorrect(string word)
        {
            return true;
        }
    }
}
