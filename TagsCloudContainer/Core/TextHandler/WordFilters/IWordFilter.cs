namespace TagsCloudContainer.Core.TextHandler.WordFilters
{
    interface IWordFilter
    {
        bool HaveToTake(string word);
    }
}