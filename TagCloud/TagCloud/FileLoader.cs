namespace TagCloud;

public class FileLoader : ITextLoader
{
    public string Path { get; }

    public FileLoader(string path) => Path = path;

    public string Load() => File.ReadAllText(Path);
}