namespace TagCloud.FileReader;

public interface IFileReader
{
    IEnumerable<string> ReadLines(string inputPath);

    IList<string> GetAvailableExtensions();
}