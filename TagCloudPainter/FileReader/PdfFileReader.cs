using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TagCloudPainter.FileReader;

public class PdfFileReader : IFileReader
{
    public IEnumerable<string> ReadFile(string path)
    {
        var list = new List<string>();
        using (var reader = new PdfReader(path))
        {
            for (var i = 1; i <= reader.NumberOfPages; i++)
            {
                var text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());

                var words = text.Replace(" ", "").Split('\n');
                foreach (var line in words)
                    if (line != "")
                        list.Add(line);
            }
        }

        return list;
    }
}