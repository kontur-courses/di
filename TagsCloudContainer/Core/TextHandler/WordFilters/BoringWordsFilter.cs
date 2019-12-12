namespace TagsCloudContainer.Core.TextHandler.WordFilters
{
    class BoringWordsFilter : IWordFilter
    {
        public bool HaveToTake(string word) => word.Length > 3;
    }
}