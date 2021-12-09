namespace TagCloud.Infrastructure.FileReader;

public class PlainTextFileReader : IFileReader
{
    private static readonly IReadOnlySet<string> SupportedExtensions = new HashSet<string> { ".txt"};

    public IEnumerable<string> GetLines(string inputPath)
    {
        if (!File.Exists(inputPath))
            throw new ArgumentException($"The file does not exist {inputPath}");

        return File.ReadLines(inputPath);
    }

    public IReadOnlySet<string> GetSupportedExtensions()
    {
        return SupportedExtensions;
    }
}