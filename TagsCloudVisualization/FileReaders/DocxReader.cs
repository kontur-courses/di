using Xceed.Words.NET;

namespace TagsCloudVisualization.FileReaders;

public class DocxReader : IFileReader
{
    private readonly string path;

    public DocxReader(string path)
    {
        this.path = path;
    }

    public string ReadText()
    {
        var doc = DocX.Load(path);
        return string.Join(" ", doc.Paragraphs.Select(p => p.Text));
    }
}