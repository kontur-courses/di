namespace TagCloudContainer.Parsers
{
    public interface IFileParser
    {
        IEnumerable<string> Parse(string text);
    }
}
