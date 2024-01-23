using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudVisualization.TextReaders;

public class DocTextReader : TextReader
{
    public DocTextReader(SourceSettings settings) : base(settings)
    {
    }

    public override string GetText()
    {
        using var document = WordprocessingDocument.Open(Settings.Path, false);
        var body = document.MainDocumentPart.Document.Body;
        return string.Join("\n", body.Descendants<Text>().Select(t => t.Text));
    }
}