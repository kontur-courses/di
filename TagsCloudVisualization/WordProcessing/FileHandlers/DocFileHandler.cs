using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Spire.Doc;

namespace TagsCloudVisualization.WordProcessing.FileHandlers
{
    public class DocFileHandler : IFileHandler
    {
        public string PathToFile { get; }
        public static readonly Regex Regex = new Regex("^.*\\.(doc|docx)$");

        public DocFileHandler(string pathToFile)
        {
            PathToFile = pathToFile;
        }
        public IEnumerable<string> ReadFile()
        {
            var doc = new Document();
            doc.LoadFromFile(PathToFile);
            var text = doc.GetText();
            return text.Split(new[] { "\r\n" }, StringSplitOptions.None);
        }
    }
}