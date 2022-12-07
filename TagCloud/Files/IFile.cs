namespace TagCloud.Files;

public interface IFile
{
    public string Path { get; }
    public string ReadAll();

    public static IFile GetByFileExtension(string filename)
    {
        var extension = filename.Split('.')[^1];
        return extension.ToLower() switch
        {
            "txt" => new TxtFile(filename),
            _ => throw new ArgumentException($"This extension {extension} are not supported!")
        };
    }
}