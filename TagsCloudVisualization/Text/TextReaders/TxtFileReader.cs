using System;
using System.Collections.Generic;
using System.IO;

namespace TagsCloudVisualization.Text.TextReaders
{
    public class TxtFileReader : ITextReader
    {
        private readonly char[] separators = {' ', '\n', '\t'};

        public TxtFileReader()
        {
        }

        public TxtFileReader(char[] separators)
        {
            this.separators = separators;
        }

        public HashSet<string> Formats { get; } = new HashSet<string> {"txt"};

        public IEnumerable<string> GetAllWords(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open))
            {
                var streamReader = new StreamReader(fileStream);
                return streamReader.ReadToEnd()
                    .Split(separators, StringSplitOptions.RemoveEmptyEntries);
            }
        }
    }
}