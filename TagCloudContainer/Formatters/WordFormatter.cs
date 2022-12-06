namespace TagCloudContainer.Formatters
{
    public class WordFormatter : IWordFormatter
    {
        public IEnumerable<string> Normalize(IEnumerable<string> textWords, Func<string, string> normalizeFunction)
        {
            return textWords.Select(normalizeFunction).ToList();
        }
    }
}
