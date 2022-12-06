namespace TagCloudContainer.Parsers
{
    internal interface IFileParser
    {
        IEnumerable<string> Parse(string text);
    }
}
