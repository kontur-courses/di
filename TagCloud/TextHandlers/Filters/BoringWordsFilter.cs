namespace TagCloud.TextHandlers
{
    public class BoringWordsFilter : IFilter
    {
        public bool IsSuit(string word)
        {
            return true;
        }
    }
}