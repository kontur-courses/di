using Spire.Doc;

namespace TagsCloudVisualization.FileReaders;

public class DocReader : IFileReader
{
    private readonly string path;

    public DocReader(string path)
    {
        this.path = path;
    }

    public string ReadText()
    {
        var doc = new Document();
        doc.LoadFromFile(path);
        var text = doc.GetText();
        return string.Join(" ", text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1));
    }
}