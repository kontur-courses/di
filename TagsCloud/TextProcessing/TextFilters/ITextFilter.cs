namespace TagsCloud.TextProcessing.TextFilters
{
    public interface ITextFilter
    {
        public bool CanTake(string word);
        public string Name { get; }
    }
}
