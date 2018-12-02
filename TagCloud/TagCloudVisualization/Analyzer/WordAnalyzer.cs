using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagCloud.Interfaces;

namespace TagCloud.TagCloudVisualization.Analyzer
{
    public class WordAnalyzer : IWordAnalyzer
    {
        public Dictionary<String, int> WeightWords(IEnumerable<String> words)
        {
            return words.GroupBy(w => w)
                .OrderByDescending(word => word.Count())
                .Take(100)
                .ToDictionary(w => w.Key, w => w.Count());
        }
        
        public IEnumerable<string> SplitWords(String text)
        {
            var wordWithoutSpecialSymbols = RemoveSpecialSymbols(text);
            return wordWithoutSpecialSymbols.Split(',', ' ').Where(p => p.Any());
        }

        public String RemoveSpecialSymbols(String text)
        {
            var textWithoutSpecialSymbols = Regex.Replace(text, "[^\\w\\._]", " ");
            return Regex.Replace(textWithoutSpecialSymbols, @"\s+", " ");
        }



    }

}
