using System.Linq;

namespace TagCloud
{
    public static class WordTokenizer
    {
        public static WordToken[] TokenizeWithNoSpeechPart(string[] words)
        {
            var frequencyDictionary = words
                .GroupBy(word => word)
                .ToDictionary(group => group.Key, group => group.Count());
            var wordTokens = frequencyDictionary
                .Select(pair => new WordToken(pair.Key, pair.Value, SpeechPartEnum.None))
                .ToArray();
            return wordTokens;
        }
    }
}
