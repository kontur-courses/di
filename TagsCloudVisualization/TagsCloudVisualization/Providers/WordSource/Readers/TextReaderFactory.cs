using System.Text.RegularExpressions;
using TagsCloudVisualization.Providers.WordSource.Interfaces;

namespace TagsCloudVisualization.Providers.WordSource.Readers
{
    internal class TextReaderFactory
    {
        private readonly DocReader docReader;
        private readonly PdfReader pdfReader;
        private readonly TxtReader txtReader;

        public TextReaderFactory()
        {
            docReader = new DocReader();
            pdfReader = new PdfReader();
            txtReader = new TxtReader();
        }

        public IFileReader GetReader(string path)
        {
            switch (path)
            {
                case var pt when new Regex(@".*txt").IsMatch(pt):
                    return txtReader;
                case var pt when new Regex(@".*pdf").IsMatch(pt):
                    return pdfReader;
                default:
                    return docReader;
            }
        }
    }
}