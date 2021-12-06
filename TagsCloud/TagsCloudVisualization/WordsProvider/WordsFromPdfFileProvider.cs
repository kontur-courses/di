using System.Collections.Generic;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace TagsCloudVisualization.WordsProvider
{
    public class WordsFromPdfFileProvider : WordsFromFileProvider
    {
        public WordsFromPdfFileProvider(string pathToFile) : base(pathToFile)
        {
        }

        protected override IEnumerable<string> GetText()
        {
            using var reader = new PdfReader(PathToFile);

            for (var i = 1; i <= reader.NumberOfPages; i++) yield return PdfTextExtractor.GetTextFromPage(reader, i);
        }
    }
}