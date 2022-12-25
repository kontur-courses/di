using System.Text.RegularExpressions;
using TagsCloudContainer.Core.WordsParser.Interfaces;
using TagsCloudContainer.Core.WordsParser.FileReaders;

namespace TagsCloudContainer.Core.WordsParser
{
    public class WordsReader : IWordsReader
    {
        private static readonly Regex ExtensionRegex = new(@".*?(?<extension>\.[^.]*?)$");
        private readonly IFileReader _fileExtensionReader; 

        public WordsReader(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File {filePath} not found.");

            var extension = GetFileExtension(filePath);

            _fileExtensionReader = extension switch
            {
                ".txt" => new TxtReader(filePath),
                ".docx" => new DocxReader(filePath),
                ".odt" => new OdtFileReader(filePath),
                _ => throw new ArgumentException($"Extensions {extension} is not support")
            };
        }
        public string? ReadWord()
        {
           return _fileExtensionReader.ReadWord();
        }
        
        private static string GetFileExtension(string filePath) => ExtensionRegex.Match(filePath).Groups["extension"].Value;
   
    }
}
