using System;
using System.Collections.Generic;
using System.Text;

namespace TagsCloud.Interfaces
{
    public interface IWordsRectanglesScaler
    {
        public SortedDictionary<double, List<string>> ConvertFreqToProportions(SortedDictionary<int, List<string>> wordsFreq);
    }
}
