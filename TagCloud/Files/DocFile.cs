using Microsoft.Office.Interop.Word;

namespace TagCloud.Files;

public class DocFile : IFile
{
    public DocFile(string path)
    {
        Path = path;
    }

    public string Path { get; }
    public const string Extension = ".docx";

    public string ReadAll()
    {
        var app = new Application();
        var doc = app.Documents.Open(Path);
        var range = doc.Range();
        return range.Text;
    }
}