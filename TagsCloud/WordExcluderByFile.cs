using System.Collections.Generic;
using System.IO;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    public class WordExcluderByFile : IWordExcluder
    {
        private IEnumerable<string> stopWords;

        public WordExcluderByFile(IFileReader fileReader, string path)
        {
            fileReader.Path = path;
            stopWords = fileReader.ReadLines();
        }

        public bool ToExclude(string word)
        {
            throw new System.NotImplementedException();
        }
    }
}