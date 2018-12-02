using System;
using System.Collections.Generic;

namespace TagCloud.Interfaces
{
    public interface IWordAnalyzer
    {
        Dictionary<String, int> WeightWords(IEnumerable<String> words);
        IEnumerable<string> SplitWords(string fileContent);
    }
}