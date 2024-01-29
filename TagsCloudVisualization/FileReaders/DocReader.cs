using Spire.Doc;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.FileReaders;

public class DocReader : IFileReader
{
    public bool CanRead(string path)
    {
        return path.Split('.')[^1] == "doc";
    }

    public string ReadText(string path)
    {
        var doc = new Document();
        doc.LoadFromFile(path);
        var text = doc.GetText();
        return string.Join(" ", text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Skip(1));
    }
}