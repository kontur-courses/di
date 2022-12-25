using AODL.Document.Content;
using AODL.Document.TextDocuments;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser.FileReaders
{
    public class OdtFileReader : IFileReader
    {
        private readonly IEnumerator<IContent>? _odtElements;
        public OdtFileReader(string filePath)
        {
            var doc = new TextDocument();
            doc.Load(filePath);
            _odtElements = doc.Content.Cast<IContent>().GetEnumerator();
        }
        public string? ReadWord()
        {
            if (_odtElements is null)
                return null;


            while (_odtElements.MoveNext())
            {
                if (_odtElements.Current.Node.InnerText.Length != 0)
                    return _odtElements.Current.Node.InnerText;
            }

            return null;
        }
    }
}
