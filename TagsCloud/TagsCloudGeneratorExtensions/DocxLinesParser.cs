using System;
using System.Linq;
using TagsCloudGenerator.Interfaces;
using Xceed.Words.NET;

namespace TagsCloudGeneratorExtensions
{
    public class DocxLinesParser : IWordsParser
    {
        public string FactorialId => "DocxLinesParser";

        public string[] ParseFromFile(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException();
            return DocX.Load(filePath)
                .Paragraphs
                .Select(p => p.Text)
                .Where(s => s.Length > 0)
                .ToArray();
        }
    }
}