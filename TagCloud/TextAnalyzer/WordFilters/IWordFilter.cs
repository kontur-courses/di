namespace TagCloud.TextAnalyzer.WordFilters
{
    public interface IWordFilter
    {
        public bool IsWordToExclude(string word);
    }
}