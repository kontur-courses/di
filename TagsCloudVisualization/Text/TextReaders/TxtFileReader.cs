using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace TagsCloudVisualization.Text.TextReaders
{
    public class TxtFileReader : ITextReader
    {
        private readonly char[] separators = { ' ', '\n' };

        public TxtFileReader() { }

        public TxtFileReader(char[] separators)
        {
            this.separators = separators;
        }

        public IEnumerable<string> GetAllWords(string filepath)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open))
            {
                var streamReader = new StreamReader(fileStream);
                var splittedText = streamReader.ReadToEnd().Split(separators).Where(x => x.Length > 0);
                foreach (var word in splittedText)
                    yield return word;
            }
        }
    }
}