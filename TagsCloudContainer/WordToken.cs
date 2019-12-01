using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    class WordToken
    {
        public string Word { get; }
        public int Count { get; }

        public WordToken(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
