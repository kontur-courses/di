using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudVisualization.TextReaders;

public class DocTextReader : TextReader
{
    public DocTextReader(string path) : base(path)
    {
    }

    public override string GetText()
    {
        using var document = WordprocessingDocument.Open(path, false);
        var body = document.MainDocumentPart.Document.Body;
        return string.Join("\n", body.Descendants<Text>().Select(t => t.Text));
    }
}