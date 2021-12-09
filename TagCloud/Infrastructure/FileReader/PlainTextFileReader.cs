namespace TagCloud.Infrastructure.FileReader;

public class PlainTextFileReader : IFileReader
{
    public IEnumerable<string> GetLines(string inputPath)
    {
        if (!File.Exists(inputPath))
            throw new ArgumentException($"The file does not exist {inputPath}");

        return File.ReadLines(inputPath);
    }
}