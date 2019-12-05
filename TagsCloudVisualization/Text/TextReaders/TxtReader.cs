using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace TagsCloudVisualization.Text.TextReaders
{
    public class TxtReader : TextReader
    {
        public TxtReader(Stream stream) : base(stream)
        {}

        private IEnumerable<string> GetAllWords()
        {
            var streamReader = new StreamReader(input);
            var splittedText = streamReader.ReadToEnd().Split(' ').Where(x => x.Length > 0);
            foreach (var word in splittedText)
                yield return word;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            return GetAllWords().GetEnumerator();
        }
    }
}