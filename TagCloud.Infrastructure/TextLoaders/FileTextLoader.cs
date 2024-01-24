public class FileTextLoader : ITextLoader
{
    public string Load(string path)
    {
        return File.ReadAllText(path);
    }
}
