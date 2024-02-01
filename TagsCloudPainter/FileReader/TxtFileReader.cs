namespace TagsCloudPainter.FileReader;

public class TxtFileReader : IFileReader
{
    public HashSet<string> SupportedExtensions => new() { ".txt" };

    public string ReadFile(string path)
    {
        return File.ReadAllText(path).Trim();
    }
}