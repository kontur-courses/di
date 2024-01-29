using DocumentFormat.OpenXml.Packaging;
using UglyToad.PdfPig;

namespace TagCloudGenerator
{
    public class TextReader
    {
        public IEnumerable<string> ReadTextFromFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            if (extension == ".docx")
                return DocxReader(filePath);                        

            if ( extension == ".pdf")
                return PdfReader(filePath);
      
            return File.ReadAllLines(filePath);
        }

        private IEnumerable<string> PdfReader(string filePath)
        {
            var text = new List<string>();
            using (var pdf = PdfDocument.Open(filePath))
            {
                foreach (var page in pdf.GetPages())
                {
                    text = page.Text.Split(' ').ToList();
                    text.Remove("");
                }
                return text;
            }
        }

        private IEnumerable<string> DocxReader(string filePath)
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
