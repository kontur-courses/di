using System.Collections.Generic;
using System.Linq;
using SautinSoft.Document;
using ElementType = SautinSoft.Document.ElementType;
using Run = SautinSoft.Document.Run;

namespace TagsCloudContainer.Readers
{
    public class DocumentsReader : IReader
    {
        public IEnumerable<string> Read(string fileName)
        {
            var docx = DocumentCore.Load(fileName);
            return Read(docx);
        }

        private IEnumerable<string> Read(DocumentCore document)
            => document.GetChildElements(true, ElementType.Paragraph)
                .SelectMany(x => x.GetChildElements(true, ElementType.Run)).Select(x => ((Run)x).Text);
    }
}