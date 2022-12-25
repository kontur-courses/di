namespace TagCloudContainer.Interfaces
{
    public interface IFileParser
    {
        IEnumerable<string> Parse(string text);
    }
}
