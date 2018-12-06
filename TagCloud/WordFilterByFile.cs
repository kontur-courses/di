using System.Collections.Generic;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class WordFilterByFile : IWordFilter
    {
        private readonly IEnumerable<string> stopWords;

        public WordFilterByFile(IFileReader fileReader, string path)
        {
            fileReader.Path = path;
            stopWords = fileReader.Read();
        }

        public bool ToExclude(string word)
        {
            return stopWords.Contains(word);
        }
    }
}