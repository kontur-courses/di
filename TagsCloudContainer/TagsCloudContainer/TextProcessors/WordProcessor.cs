using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class WordProcessor:IWordProcessor
    {
        public WordProcessor(HashSet<string> boringWords)
        {
            this.boringWords = boringWords;
        }

        private HashSet<string> boringWords;


        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word)).Select(word => word.ToLower());
        }
    }
}
