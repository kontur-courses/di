using System.Collections.Generic;

namespace TagCloud
{
    internal interface IWordsProcessor
    {
        public Dictionary<string, double> Process(IEnumerable<string> words);
    }
}
