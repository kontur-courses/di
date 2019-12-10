using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TextParser : IParser
    {
        public IEnumerable<WordToken> ParseToTokens(string text, HashSet<string> boringWords)
        {
            var wordCountDictionary = new Dictionary<string, int>();
            var splittedText = text.Split('\n').Select(word => word.ToLower());
            foreach (var lineWord in splittedText)
            {
                if (boringWords.Contains(lineWord) || string.IsNullOrEmpty(lineWord))
                    continue;

                if (!wordCountDictionary.ContainsKey(lineWord))
                    wordCountDictionary.Add(lineWord, 1);
                else
                    wordCountDictionary[lineWord] += 1;
            }
            foreach (var kvp in wordCountDictionary)
                yield return new WordToken(kvp.Key, kvp.Value);
        }
    }
}