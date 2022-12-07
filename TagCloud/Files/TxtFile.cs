namespace TagCloud.Files;

public class TxtFile : IFile
{
    public TxtFile(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public string ReadAll()
    {
        using var stream = new StreamReader(Path);
        var text = stream.ReadToEnd();
        return text;
    }
}