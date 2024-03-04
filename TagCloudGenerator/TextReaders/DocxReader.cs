using DocumentFormat.OpenXml.Packaging;

namespace TagCloudGenerator.TextReaders
{
    public class DocxReader : ITextReader
    {
        public string GetFileExtension() => ".docx";

        public IEnumerable<string> ReadTextFromFile(string filePath)
        {
            using (WordprocessingDocument wordDocument =
                      WordprocessingDocument.Open(filePath, false))
            {
                var body = wordDocument.MainDocumentPart.Document.Body;
                var paragraphs = body.ChildElements;

                var text = new List<string>(paragraphs.Count);
                foreach (var paragraph in paragraphs)
                {
                    if (paragraph.InnerText != "")
                        text.Add(paragraph.InnerText);
                }

                return text;
            }
        }
    }
}