using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser.FileReaders
{
    public class DocxReader : IFileReader
    {
        private readonly IEnumerator<OpenXmlElement>? _docxElements;

        public DocxReader(string filePath)
        {
            _docxElements = WordprocessingDocument.Open(filePath, true)
                .MainDocumentPart?.Document.Body?.GetEnumerator();
        }

        public string? ReadWord()
        {
            if (_docxElements is null)
                return null;

            while (_docxElements.MoveNext())
            {
                if (_docxElements.Current.InnerText.Length != 0)
                    return _docxElements.Current.InnerText;
            }

            return null;
        }
    }
}
