namespace TagCloudContainer.Parsers
{
    public class FileLinesParser : IFileParser
    {
        public IEnumerable<string> Parse(string text)
        {
            return text.Split(Environment.NewLine);
        }
    }
}
