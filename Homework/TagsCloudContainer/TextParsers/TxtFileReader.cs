using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TagsCloudContainer.TextParsers
{
    public class TxtFileReader : ISourceReader
    {
        private readonly string[] splitRules;
        private readonly string path;

        public TxtFileReader(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException("No such source file!");
            splitRules = new []{ " ", ". ", "! ", "\n", "? ", "... ", ", ", ": ", "; ", "\" ", "-", "- "};
            this.path = path;
        }

        public IEnumerable<string> GetNextWord()
        {
            using (var sr = new StreamReader(path, Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                    foreach (var word in line.Split(splitRules, StringSplitOptions.RemoveEmptyEntries))
                        yield return word;
            }
        }
    }
}
