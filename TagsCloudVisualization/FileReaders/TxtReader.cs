namespace TagsCloudVisualization.FileReaders;

public class TxtReader : IFileReader
{
    private readonly string path;

    public TxtReader(string path)
    {
        this.path = path;
    }

    public string ReadText()
    {
        return File.ReadAllText(path);
    }
}