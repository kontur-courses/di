using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud
{
    public  class WordsHandler : IWordsHandler
    {
        public  Dictionary<string, int> Conversion(Dictionary<string, int> wordsAndCount)
        {
            return new Dictionary<string, int>();
        }

        public  Dictionary<string, int> GetWordsAndCount(string path)
        {
            return new Dictionary<string, int>();
        }
    }
}
