using System.Collections.Generic;
using TagsCloudVisualization.TextReaders;

namespace TagsCloudVisualization.Storages
{
    internal class WordStorage : IWordStorage
    {
        public IEnumerable<string> Words { get; set; }

        public WordStorage(IEnumerable<string> words)
        {
            Words = words;
        }

        public WordStorage(ITextReader reader)
        { 
            Words = reader.Read();
        }
    }
}
