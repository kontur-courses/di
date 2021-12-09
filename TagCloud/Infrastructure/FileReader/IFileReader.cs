namespace TagCloud.Infrastructure.FileReader;

public interface IFileReader
{
    IEnumerable<string> GetLines(string inputPath);

    IReadOnlySet<string> GetSupportedExtensions();
}