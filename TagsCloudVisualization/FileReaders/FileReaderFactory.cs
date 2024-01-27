namespace TagsCloudVisualization.FileReaders;

public class FileReaderFactory : IFileReaderFactory
{
    public IFileReader Create(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException($"File at this path {Path.GetFullPath(path)} doesn't exist");
        }
        var extension = path.Split('.')[^1];
        return extension switch
        {
            "txt" => new TxtReader(path),
            "doc" => new DocReader(path),
            "docx" => new DocxReader(path),
            _ => throw new ArgumentException($"File with extension {extension} doesn't supported")
        };
    }
}