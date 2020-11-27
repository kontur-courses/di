namespace TagCloud.Core.WordsFilters
{
    public interface IWordFilter
    {
        public bool IsValidWord(string word);
    }
}