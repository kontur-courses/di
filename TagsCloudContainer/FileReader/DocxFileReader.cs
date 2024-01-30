using System.Text;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using TagsCloudContainer.FileReader.Interfaces;

namespace TagsCloudContainer.FileReader;
public class DocxFileReader : ITextReader
{
    public string GetTextFromFile(string filePath)
    {
        var sb = new StringBuilder();
        using (var doc = WordprocessingDocument.Open(filePath, false))
        {
            var body = doc.MainDocumentPart?.Document.Body;

            foreach (var paragraph in body?.Elements<Paragraph>()!)
            {
                var paragraphText = paragraph.InnerText.ToLower();
                sb.AppendLine(paragraphText);
            }
        }

        return sb.ToString();
    }
}