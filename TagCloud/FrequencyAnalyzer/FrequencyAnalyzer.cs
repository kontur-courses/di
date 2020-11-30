using System;
using System.Collections.Generic;
using System.Linq;

namespace TagCloud.FrequencyAnalyzer
{
    public class FrequencyAnalyzer: IFrequencyAnalyzer
    {
        private readonly IWordParser parser;
        
        public FrequencyAnalyzer(IWordParser wordParser)
        {
            parser = wordParser;
        }
        
        public Dictionary<string, double> GetFrequencyDictionary(string fileName)
        {
            var words = parser.GetWords(fileName);
            
            return words
                .GroupBy(str => str)
                .ToDictionary(group => group.Key,
                    group => group.Count() / (double) words.Length);
        }
    }
}