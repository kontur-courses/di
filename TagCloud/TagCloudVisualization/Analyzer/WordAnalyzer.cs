using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagCloud.TagCloudVisualization.Analyzer
{
    public class WordAnalyzer
    {
        public static Dictionary<String, int> WeightWords(IEnumerable<String> words)
        {
            return words.GroupBy(w => w)
                .OrderByDescending(word => word.Count())
                .ToDictionary(w => w.Key, w => w.Count());
        }
        
        public static IEnumerable<string> SplitWords(String text)
        {
            var wordWithoutSpecialSymbols = RemoveSpecialSymbols(text);
            return wordWithoutSpecialSymbols.Split(',', ' ').Where(p => p.Any());
        }

        public static String RemoveSpecialSymbols(String text)
        {
            var textWithoutSpecialSymbols = Regex.Replace(text, "[^\\w\\._]", " ");
            return Regex.Replace(textWithoutSpecialSymbols, @"\s+", " ");
        }



    }

}
