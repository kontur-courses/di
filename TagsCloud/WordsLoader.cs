using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagsCloud.FileParsers;

namespace TagsCloud
{
    public class WordsLoader
    {
        private readonly IFileParser[] parsers;

        public WordsLoader(IFileParser[] parsers)
        {
            this.parsers = parsers;
        }

        public List<string> LoadWords(string filename)
        {
            var fileExtension = Path.GetExtension(filename);
            var fileParser = parsers.FirstOrDefault(p =>
                p.FileExtensions.Any(ext => ext == fileExtension));
            if (fileParser == null)
                throw new ArgumentException($"Can't select file parser for this file format ({filename})");
            return fileParser.Parse(filename);
        }
    }
}
