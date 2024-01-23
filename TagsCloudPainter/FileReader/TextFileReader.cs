using Spire.Doc;

namespace TagsCloudPainter.FileReader;

public class TextFileReader : IFileReader
{
    public string ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException();

        return Path.GetExtension(path) switch
        {
            ".txt" => ReadTxtFile(path),
            ".doc" => ReadDocFile(path),
            ".docx" => ReadDocFile(path),
            _ => throw new ArgumentException("Incorrect file extension. Supported file extensions: txt, doc, docx")
        };
    }

    private static string ReadTxtFile(string path)
    {
        return File.ReadAllText(path).Trim();
    }

    private static string ReadDocFile(string path)
    {
        var doc = new Document();
        doc.LoadFromFile(path);
        var text = doc.GetText();
        var lastIndexOfSpirePart = text.IndexOf(Environment.NewLine);
        return text.Substring(lastIndexOfSpirePart + 2).Trim();
    }
}