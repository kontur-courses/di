namespace TagsCloudPainter.FileReader;

public interface IFileReader
{
    public HashSet<string> SupportedExtensions { get; }
    public string ReadFile(string path);
}