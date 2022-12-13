using System.Text.RegularExpressions;
using TagsCloudContainer.Core.WordsParser.Interfaces;

namespace TagsCloudContainer.Core.WordsParser
{
    public class FileReader : IWordsReader
    {
        private delegate string? WordsReaderMethod();
        private static readonly Regex ExtensionRegex = new(@".*?(?<extension>\.[^.]*?)$");
        private readonly WordsReaderMethod _wordsReader;
        private readonly StreamReader _streamReader;

        public FileReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File {filePath} not found.");

            _streamReader = new StreamReader(filePath);
            var extension = GetFileExtension(filePath);

            _wordsReader = extension switch
            {
                ".txt" => ReadWordFromTxt,
                _ => throw new ArgumentException($"Extensions {extension} is not support")
            };
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
        private static string GetFileExtension(string filePath) => ExtensionRegex.Match(filePath).Groups["extension"].Value;
    }
}
