using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TagCloudPainter.FileReader;

public class PdfFileReader : IFileReader
{
    public IEnumerable<string> ReadFile(string path)
    {
        using (var reader = new PdfReader(path))
        {
            var sb = new StringBuilder();
            for (var i = 1; i <= reader.NumberOfPages; i++)
            {
                var text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                sb.Append(text);
            }

            return sb.ToString().Replace(" ", "").Split('\n').Where(x => x != "").ToList();
        }
    }
}