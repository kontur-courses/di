using System;
using System.Text.RegularExpressions;
using TagsCloudVisualization.WordSource.Interfaces;

namespace TagsCloudVisualization.WordSource.Readers
{
    internal class TextReaderFactory
    {
        private TxtReader txtReader;
        private PdfReader pdfReader;
        private DocReader docReader;

        public TextReaderFactory()
        {
            this.docReader = new DocReader();
            this.pdfReader = new PdfReader();
            this.txtReader = new TxtReader();
        }

        public IFileReader<string> GetReader(string path)
        {
            switch (path)
            {
                case var pt when new Regex(@".*txt").IsMatch(pt):
                    return txtReader;
                case var pt when new Regex(@".*pdf").IsMatch(pt):
                    return pdfReader;
                case var pt when new Regex(@".*doc").IsMatch(pt):
                    return docReader;
            }
            throw  new ArgumentException($"Unexpected Format on path : {path}");
        }

        
    }
}