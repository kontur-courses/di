namespace TagsCloudVisualization.FileReaders;

public interface IFileReaderFactory
{
    IFileReader Create(string path);
}