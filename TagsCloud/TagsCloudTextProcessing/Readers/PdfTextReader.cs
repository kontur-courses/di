using BitMiracle.Docotic.Pdf;

namespace TagsCloudTextProcessing.Readers
{
    public class PdfTextReader : ITextReader
    {
        private readonly string path;

        public PdfTextReader(string path)
        {
            this.path = path;
        }

        public string ReadText()
        {
            using (var pdfDocument = new PdfDocument(path))
                return pdfDocument.GetText();
        }
    }
}