namespace TagsCloudContainer.Infrastructure.TextAnalyzer
{
    internal interface IWordFilter
    {
        public bool IsBoring(string word);
    }
}