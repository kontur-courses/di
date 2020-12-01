namespace TagsCloud.TextProcessing.TextFilters
{
    public interface ITextFilter
    {
        bool CanTake(string word);
        string Name { get; }
    }
}
