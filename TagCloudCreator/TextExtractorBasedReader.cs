using System;
using System.IO;
using RS.TextExtractor;

namespace TagCloudCreator
{
    public class TextExtractorBasedReader : IFileReader
    {
        public string[] Types => new[] {".doc", ".docx", ".html", ".pdf", ".txt"};

        public string[] ReadAllLinesFromFile(string path)
        {
            return Extractor.ExtractTextFromFile(path, File.ReadAllBytes(path)).Split(new[] {'\n', '\r', ' ', '\t'},
                StringSplitOptions.RemoveEmptyEntries);
        }
    }
}