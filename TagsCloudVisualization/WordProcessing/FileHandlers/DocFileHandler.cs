using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Code7248.word_reader;

namespace TagsCloudVisualization.WordProcessing.FileHandlers
{
    public class DocFileHandler : FileHandler
    {
        public string PathToFile { get; }
        public static readonly Regex Regex = new Regex("^.*\\.(doc|docx)$");

        public DocFileHandler(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        public IEnumerable<string> ReadFile()
        {
            var extractor = new TextExtractor(PathToFile);
            var text = extractor.ExtractText();
            return text.Split(new[] { "\r\n\r\n" }, StringSplitOptions.None);
        }
    }
}