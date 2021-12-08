using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class TextFileWordsContainer : IWordsContainer
    {
        private IWordReader wordReader;
        private IWordProcessor wordProcessor;
        private string path;
        public TextFileWordsContainer(IWordReader wordReader, IWordProcessor wordProcessor, string path)
        {
            this.wordReader = wordReader;
            this.wordProcessor = wordProcessor;
            this.path = path;
        }

        public Dictionary<string, int> GetWords()
        {
            var words = wordReader.Read(path);
            words = wordProcessor.Process(words);

            return words.GroupBy(word => word).ToDictionary(w => w.Key, w => w.Count());
        }
    }
}
