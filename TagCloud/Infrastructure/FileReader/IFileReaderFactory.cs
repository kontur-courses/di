namespace TagCloud.Infrastructure.FileReader;

public interface IFileReaderFactory
{
    IFileReader Create(string filePath);
}