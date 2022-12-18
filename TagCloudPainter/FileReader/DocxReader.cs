using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagCloudPainter.FileReader;

public class DocxReader : IFileReader
{
    public IEnumerable<string> ReadFile(string path)
    {
        using (var wordDocument = WordprocessingDocument.Open(path, false))
        {
            var paragraphs = wordDocument.MainDocumentPart.Document.Body.Descendants<Paragraph>();
            return paragraphs.Select(x => x.InnerText).ToList();
        }
    }
}