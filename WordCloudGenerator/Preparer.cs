using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCloudGenerator
{
    public class Preparer
    {
        private readonly HashSet<string> wordsToSkip;

        public Preparer(IEnumerable<string> wordsToSkip)
        {
            this.wordsToSkip = wordsToSkip == null ? new HashSet<string>() : wordsToSkip?.ToHashSet();
        }

        public Dictionary<string, int> GetCountedWords(string inputText)
        {
            var text = inputText.ToLower();
            var wordsInText = Regex.Matches(text, @"\w+")
                .Select(match => match.Value)
                .Where(word => !wordsToSkip.Contains(word)).ToArray();
            
            return wordsInText.Distinct().ToDictionary(
                word => word, 
                word => wordsInText.Count(el => el == word));
        }
    }
}