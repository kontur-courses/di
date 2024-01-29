namespace TagsCloudVisualization.FileReaders;

public interface IFileReader
{
    bool CanRead(string path);
    string ReadText(string path);
}
