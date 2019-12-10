using BitMiracle.Docotic.Pdf;

namespace TagsCloudTextProcessing.Readers
{
    public class PdfTextReader : ITextReader
    {
        public string ReadText(string path)
        {
            using (var pdfDocument = new PdfDocument(path))
                return pdfDocument.GetText();
        }
    }
}