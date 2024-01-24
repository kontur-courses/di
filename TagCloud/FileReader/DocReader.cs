using Spire.Doc;

namespace TagCloud.FileReader;

public class DocReader : IFileReader
{
    public IEnumerable<string> ReadLines(string inputPath)
    {
        if (!File.Exists(inputPath))
            throw new ArgumentException("Source file doesn't exist");

        var document = new Document(inputPath, FileFormat.Auto);
        var text = document.GetText();

        return text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1);
    }

    public IList<string> GetAvailableExtensions() => new List<string>() { "doc", "docx" };
}