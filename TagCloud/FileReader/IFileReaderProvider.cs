namespace TagCloud.FileReader;

public interface IFileReaderProvider
{
    IFileReader CreateReader(string inputPath);
}