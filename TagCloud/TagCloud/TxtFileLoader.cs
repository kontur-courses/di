namespace TagCloud;

public class TxtFileLoader : IFileLoader
{
    public string Load(string path)
    {
        if (string.IsNullOrEmpty(path))
            throw new ArgumentException("Path is null or empty");
        if (!File.Exists(path))
            throw new FileNotFoundException($"File with path \"{path}\" isn't found: ");
        if (Path.GetExtension(path) != ".txt")
            throw new ArgumentException("File format is not .txt");
        return File.ReadAllText(path);
    }
}