using System;
using TikaOnDotNet.TextExtraction;

namespace TagCloudCreator
{
    public class TikaBasedReader : IFileReader
    {
        public string[] Types => new[] {".doc", ".docx", ".html", ".pdf", ".md", ".txt"};

        public string[] ReadAllLinesFromFile(string path)
        {
            var app = new TextExtractor();
            var text = app.Extract(path).Text.Split(new[] {Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
            return text;
        }
    }
}