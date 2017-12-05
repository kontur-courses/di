using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    public class FileReader : IReader
    {
        private readonly string filename;

        public FileReader(string filename)
        {
            this.filename = filename;
        }

        public IEnumerable<string> ReadWords()
        {
            return File.ReadLines(filename)
                .SelectMany(line => line.Split(
                    new Char [] {' ', ',', '.', ':', ';', '!', '?', '\t', '–', '"'}, 
                    StringSplitOptions.RemoveEmptyEntries));
        }
    }
}