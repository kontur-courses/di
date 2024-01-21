using Microsoft.Office.Interop.Word;

namespace TagsCloudVisualization.FileReader;

public class DocReader : IFileReader
{
    public string ReadText(string path)
    {
        var application = new Application();
        path = Path.GetFullPath(path);
        var doc = application.Documents.Open(path);
        var content = doc.Content.Text;
        doc.Close();
        return content;
    }
}