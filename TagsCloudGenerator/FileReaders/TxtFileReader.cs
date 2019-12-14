using System.Collections.Generic;
using System.IO;
using TagsCloudGenerator.Tools;

namespace TagsCloudGenerator.FileReaders
{
    public class TxtFileReader : IFileReader
    {
        private readonly IWordsParser parser;

        public TxtFileReader(IWordsParser parser)
        {
            this.parser = parser;
        }

        public Dictionary<string, int> ReadWords(string path)
        {
            var text = File.ReadAllText(path);
            var words = parser.Parse(text);

            return ParseHelper.GetWordToCount(words);
        }
    }
}