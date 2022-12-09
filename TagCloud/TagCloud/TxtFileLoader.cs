namespace TagCloud;

public class TxtFileLoader : IFileLoader
{
    public string Load(string path)
    {
        return File.ReadAllText(path);
    }
}