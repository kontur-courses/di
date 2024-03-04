namespace TagCloudGenerator.TextReaders
{
    public interface ITextReader
    {
        IEnumerable<string> ReadTextFromFile(string filePath);
        string GetFileExtension();
    }
}