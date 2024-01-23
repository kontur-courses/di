namespace TagsCloudVisualization.FileReaders;

public class FileReader : IFileReader
{
    public string ReadText(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File at this path {Path.GetFullPath(path)} doesn't exist");
        }
        var extension = path.Split('.')[^1];
        IFileReader reader = extension switch
        {
            "txt" => new TxtReader(),
            "doc" => new DocReader(),
            "docx" => new DocxReader(),
             _ => throw new ArgumentException($"File with extension {extension} doesn't supported")
        };
        return reader.ReadText(path);
    }
}
