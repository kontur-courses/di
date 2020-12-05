using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;

namespace TagsCloud.FileReaders
{
    public class DocxFileReader : IFileReader
    {
        public IEnumerable<string> GetWordsFromFile(string filePath)
        {
            var document = WordprocessingDocument.Open(filePath, false);

            var documentBody = document.MainDocumentPart.Document.Body;

            return documentBody.Select(item => item.InnerText);
        }
    }
}
