using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudContainer.FileReaders
{
    public class DocFileReader : IFileReader
    {
        public HashSet<string> SupportedFormats { get; } = new HashSet<string>(){ ".doc", ".docx" };

        public IEnumerable<string> ReadWordsFromFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            
            using var doc = WordprocessingDocument.Open(path, false);
            return doc.MainDocumentPart.Document.Body.Descendants<Paragraph>()
                .Select(x => x.InnerText)
                .Where(x => x != string.Empty);
        }
    }
}