using Spire.Doc;

namespace TagsCloudVisualization.FileReaders;

public class DocReader : IFileReader
{
    public string ReadText(string path)
    {
        var doc = new Document();
        doc.LoadFromFile(path);
        var text = doc.GetText();
        return string.Join(" ", text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1));
    }
}
