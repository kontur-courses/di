namespace TagCloudContainer.Formatters
{
    internal interface IWordFormatter
    {
        IEnumerable<string> Normalize(IEnumerable<string> textWords, Func<string, string> normalizeFunction);
    }
}
