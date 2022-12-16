namespace TagCloudPainter.FileReader;

public interface IFileReader
{
    public IEnumerable<string> ReadFile(string path);
}