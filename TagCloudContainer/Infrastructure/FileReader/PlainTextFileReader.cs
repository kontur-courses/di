namespace TagCloudContainer.Infrastructure.FileReader;

public class PlainTextFileReader : IFileReader
{
    public IEnumerable<string> GetLines(string filePath)
    {
        if (!File.Exists(filePath))
            throw new ArgumentException("The file does not exist");

        return File.ReadLines(filePath);
    }
}