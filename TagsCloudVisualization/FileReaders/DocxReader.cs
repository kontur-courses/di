using Xceed.Words.NET;

namespace TagsCloudVisualization.FileReaders;

public class DocxReader : IFileReader
{
    public bool CanRead(string path)
    {
        return path.Split('.')[^1] == "docx";
    }

    public string ReadText(string path)
    {
        var doc = DocX.Load(path);
        return string.Join(" ", doc.Paragraphs.Select(p => p.Text));
    }
}