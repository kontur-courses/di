using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TagsCloud.Visualization.WordsReaders.FileReaders
{
    public class PdfFileReader : IFileReader
    {
        public string Read(string filename)
        {
            var text = new StringBuilder();

            using var reader = new PdfReader(filename);

            for (var i = 1; i <= reader.NumberOfPages; i++) text.Append(PdfTextExtractor.GetTextFromPage(reader, i));

            return text.ToString();
        }

        public bool CanRead(string extension) => extension == "pdf";
    }
}