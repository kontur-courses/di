namespace TagsCloud;

public class FileReader
{
    private readonly Dictionary<string, IParser> parsers;

    public FileReader(IEnumerable<IParser> parsers)
    {
        this.parsers = parsers.ToDictionary(parser => parser.FileType);
    }

    public IEnumerable<string?> GetWords(string filePath)
    {
        if (!File.Exists(filePath))
            throw new ArgumentException($"Error: File not found at the specified path '{filePath}'.");

        var fileType = Path.GetExtension(filePath).Trim('.').ToLower();
        if (!parsers.ContainsKey(fileType))
            throw new ArgumentException($"not found parser for {fileType}");

        return parsers[fileType].GetWordList(filePath);
    }
}