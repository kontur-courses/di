using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagCloudPainter.FileReader;

public class DocReader : IFileReader
{
    public IEnumerable<string> ReadFile(string path)
    {
        var words = new List<string>();
        using (var wordDocument = WordprocessingDocument.Open(path, false))
        {
            var paragraphs = wordDocument.MainDocumentPart.Document.Body.Descendants<Paragraph>();
            foreach (var paragraph in paragraphs) 
                words.Add(paragraph.InnerText);
        }

        return words;
    }
}