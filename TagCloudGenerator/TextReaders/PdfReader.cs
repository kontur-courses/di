using UglyToad.PdfPig;

namespace TagCloudGenerator.TextReaders
{
    public class PdfReader : ITextReader
    {
        public string GetFileExtension() => ".pdf";
       
        public IEnumerable<string> ReadTextFromFile(string filePath)
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
    }
}