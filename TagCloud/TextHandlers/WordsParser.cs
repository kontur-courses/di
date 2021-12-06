using System;
using System.Collections.Generic;
using System.IO;
using TagsCloudVisualization;

namespace TagCloud.TextHandlers
{
    public class WordsParser : ITextParser
    {
        private readonly string path;

        public WordsParser(string path)
        {
            this.path = path;
        }

        public IEnumerable<string> GetWords()
        {
            return File.ReadAllLines(path);
        }
    }
}