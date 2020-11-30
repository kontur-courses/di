using System.Collections.Generic;
using System.IO;
using System.Linq;
using TagCloud.Interfaces;

namespace TagCloud
{
    public class WordsReader : IWordsReader
    {
        public List<string> Get(string path)
        {
            using (var fileStream = new StreamReader(path))
            {
                return fileStream.ReadToEnd().Split('\n').ToList();
            }
        }
    }
}