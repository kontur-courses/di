using System.Collections.Generic;
using System.Linq;
using App.Infrastructure.FileInteractions.Readers;
using DocumentFormat.OpenXml.Packaging;

namespace App.Implementation.FileInteractions.Readers
{
    public class FromDocReader : ILinesReader
    {
        private readonly string fileName;

        public FromDocReader(string fileName)
        {
            this.fileName = fileName;
        }

        public IEnumerable<string> ReadLines()
        {
            return WordprocessingDocument
                .Open(fileName, false)
                .MainDocumentPart?
                .Document
                .Body?.Select(item => item.InnerText);
        }
    }
}