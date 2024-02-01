using Spire.Doc;

namespace TagsCloudPainter.FileReader;

public class DocFileReader : IFileReader
{
    public HashSet<string> SupportedExtensions => new() { ".doc", ".docx" };

    public string ReadFile(string path)
    {
        var doc = new Document();
        doc.LoadFromFile(path);
        var text = doc.GetText();
        var lastIndexOfSpirePart = text.IndexOf(Environment.NewLine);
        return text.Substring(lastIndexOfSpirePart + 2).Trim();
    }
}