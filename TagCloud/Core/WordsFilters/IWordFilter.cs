namespace TagCloud.Core.WordsFilters
{
    public interface IWordFilter
    {
        public bool IsValid(string word);
    }
}