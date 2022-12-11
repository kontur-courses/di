using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface IWordsFrequencyAnalyzer
    {
        public SortedDictionary<int, List<string>> GetSortedDictOfWordsFreq(IEnumerable<string> normalFormWords);
    }
}
