using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TagsCloudVisualization
{
    public class WordAnalyzer
    {
        public Dictionary<String, int> WeightWords(List<String> words)
        {
            return words.GroupBy(w => w)
                .OrderByDescending(word => word.Count())
                .ToDictionary(w => w.Key, w => w.Count());
        }

        public List<string> TextAnalyzer(String text)
        {
            var wordWithoutSpecialSymbols = RemoveSpecialSymbols(text);
            return wordWithoutSpecialSymbols.Split(' ').Where(x => x != String.Empty).ToList();
        }

        public String RemoveSpecialSymbols(String text)
        {
            var textWithoutSpecialSymbols = Regex.Replace(text, "[^\\w\\._]", " ");
            return Regex.Replace(textWithoutSpecialSymbols, @"\s+", " ");
        }


    }

}
