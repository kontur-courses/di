using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.WordProcessor
{
    public class WordWithCount
    {
        public string Word { get; }
        public int Count { get; }

        public WordWithCount(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
