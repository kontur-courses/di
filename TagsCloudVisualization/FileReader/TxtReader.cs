namespace TagsCloudVisualization.FileReader;

public class TxtReader : IFileReader
{
    public string ReadText(string path)
    {
        return File.ReadAllText(path);
    }
}
