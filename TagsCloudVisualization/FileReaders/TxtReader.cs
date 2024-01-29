namespace TagsCloudVisualization.FileReaders;

public class TxtReader : IFileReader
{
    public bool CanRead(string path)
    {
        return path.Split('.')[^1] == "txt";
    }

    public string ReadText(string path)
    {
        return File.ReadAllText(path);
    }
}