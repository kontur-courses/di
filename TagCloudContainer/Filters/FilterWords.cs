namespace TagCloudContainer.Filters
{

    public class FilterWords : IFilter
    {
        public IEnumerable<string> Filter(IEnumerable<string> textWords, Func<string, bool> filterFunc)
        {
            return textWords.Select(x => x).Where(filterFunc);
        }
    }
}
