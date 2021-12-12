namespace TagsCloudVisualization.Common.WordFilters
{
    public interface IWordFilter
    {
        public bool IsValid(string word);
    }
}