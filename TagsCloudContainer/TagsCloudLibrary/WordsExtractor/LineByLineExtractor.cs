using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudLibrary.WordsExtractor
{
    public class LineByLineExtractor : IWordsExtractor
    {
        public IEnumerable<string> ExtractWords(Stream stream)
        {
            var words = new List<string>();

            using (var sr = new StreamReader(stream))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    words.Add(line.Trim());
                }
            }

            return words;
        }
    }
}
