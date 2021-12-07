namespace TagCloud.TextHandlers.Filters
{
    public class BoringWordsFilter : IFilter
    {
        public bool IsSuit(string word)
        {
            return word.Length > 3;
        }
    }
}