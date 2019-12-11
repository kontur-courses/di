using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Services;

namespace TagsCloudVisualization.Logic
{
    public class TextParser : IParser
    {
        private static readonly char[] Separators = {'\n', '\r'};
        private readonly IBoringWordsProvider boringWordsProvider;

        public TextParser(IBoringWordsProvider boringWordsProvider)
        {
            this.boringWordsProvider = boringWordsProvider;
        }

        public IEnumerable<WordToken> ParseToTokens(string text)
        {
            if (text == null)
                throw new ArgumentNullException();
            var wordCountDictionary = new Dictionary<string, int>();
            var splittedText = text
                .Split(Separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => word.ToLower());
            foreach (var lineWord in splittedText)
            {
                if (IsWordInvalid(lineWord))
                    continue;
                if (!wordCountDictionary.ContainsKey(lineWord))
                    wordCountDictionary.Add(lineWord, 1);
                else
                    wordCountDictionary[lineWord] += 1;
            }
            foreach (var kvp in wordCountDictionary)
                yield return new WordToken(kvp.Key, kvp.Value);
        }

        private bool IsWordInvalid(string word)
        {
            return boringWordsProvider.BoringWords != null
                   && boringWordsProvider.BoringWords.Contains(word)
                   || string.IsNullOrEmpty(word);
        }
    }
}