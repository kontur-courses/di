namespace TagsCloudPainter.FileReader;

public class TextFileReader : IFileReader
{
    private readonly DocFileReader docFileReader = new();
    private readonly TxtFileReader txtFileReader = new();

    public string ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

        return Path.GetExtension(path) switch
        {
            ".txt" => txtFileReader.ReadFile(path),
            ".doc" => docFileReader.ReadFile(path),
            ".docx" => docFileReader.ReadFile(path),
            _ => throw new ArgumentException("Incorrect file extension. Supported file extensions: txt, doc, docx")
        };
    }
}