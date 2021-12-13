using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TagsCloudVisualization.WordsProvider.FileReader
{
    public class PdfFileReader : IWordsReader
    {
        public bool IsSupportedFileExtension(string extension) => extension == "pdf";

        public string GetFileContent(string path)
        {
            var text = new StringBuilder();

            using var reader = new PdfReader(path);
            
            for (var i = 1; i <= reader.NumberOfPages; i++)
            {
                text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
            }

            return text.ToString();
        }
    }
}