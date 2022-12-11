namespace TagCloudContainer.Formatters
{
    public interface IWordFormatter
    {
        IEnumerable<string> Normalize(IEnumerable<string> textWords, Func<string, string> normalizeFunction);
    }
}
