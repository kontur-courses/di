using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using AODL.Document.Content;
using AODL.Document.TextDocuments;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser
{
    public class FileReader : IWordsReader
    {
        private delegate string? WordsReaderMethod();
        private static readonly Regex ExtensionRegex = new(@".*?(?<extension>\.[^.]*?)$");
        private readonly WordsReaderMethod _wordsReader;

        private readonly StreamReader _streamReader;
        private readonly IEnumerator<OpenXmlElement>? _docxElements;
        private readonly IEnumerator<IContent>? _odtElements;

        private readonly string _filePath;

        public FileReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File {filePath} not found.");

            _filePath = filePath;
            var extension = GetFileExtension(_filePath);

            switch (extension)
            {
                case ".txt":
                {
                    _wordsReader = ReadWordFromTxt;
                    _streamReader = new StreamReader(_filePath);
                    break;
                }
                case ".docx":
                {
                    _wordsReader = ReadWordFromDocx;
                    _docxElements = WordprocessingDocument.Open(_filePath, true).MainDocumentPart?.Document.Body?.GetEnumerator();
                    break;
                }
                case ".odt":
                {
                    _wordsReader = ReadWordFromOdt;
                    var doc = new TextDocument();
                    doc.Load(_filePath);
                    _odtElements = doc.Content.Cast<IContent>().GetEnumerator();
                    break;
                }
                default:
                {
                   throw new ArgumentException($"Extensions {extension} is not support");
                }
            }
        }

        public string? ReadWord() => _wordsReader();

        private string? ReadWordFromTxt()
        {
            while (_streamReader.Peek() >= 0)
            {
                var word = _streamReader.ReadLine()?.Trim();

                if (word is null)
                    return null;

                if (word.Length != 0)
                    return word;
            }

            return null;
        }

        private string? ReadWordFromDocx()
        {
            if(_docxElements is null)
                return null;

            while (_docxElements.MoveNext())
            {
                if (_docxElements.Current.InnerText.Length != 0)
                    return _docxElements.Current.InnerText;
            }

            return null;
        }

        private string? ReadWordFromOdt()
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

        private static string GetFileExtension(string filePath) => ExtensionRegex.Match(filePath).Groups["extension"].Value;
    }
}
