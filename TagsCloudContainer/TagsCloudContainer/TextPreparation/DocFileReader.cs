using System;
using System.Collections.Generic;
using System.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace TagsCloudContainer.TextPreparation
{
    public class DocFileReader : IFileReader
    {
        private readonly IWordsReader wordsReader;

        public DocFileReader(IWordsReader wordsReader)
        {
            this.wordsReader = wordsReader;
        }

        public List<string> GetAllWords(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentException("Path can't be null");
            }

            using var document = WordprocessingDocument.Open(filePath, false);
            var fileContent = string.Join(Environment.NewLine,
                document.MainDocumentPart?.RootElement?.Descendants<Paragraph>().Select(p => p.InnerText).ToList()!);
            return wordsReader.ReadAllWords(fileContent);
        }
    }
}