using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Preprocessing
{
    public class WordsPreprocessor
    {
        private readonly HashSet<SpeechPart> speechPartsToRemove = new()
        {
            SpeechPart.INTJ,
            SpeechPart.PART,
            SpeechPart.PR,
            SpeechPart.CONJ,
        };

        private readonly IWordInfoParser wordInfoParser;

        public WordsPreprocessor(IWordInfoParser wordInfoParser)
        {
            this.wordInfoParser = wordInfoParser;
        }

        public HashSet<WordInfo> Process(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var uniqueWords = GetUniqueWords(words);
            return wordInfoParser.ParseWords(uniqueWords)
                .Where(wordInfo => !speechPartsToRemove.Contains(wordInfo.SpeechPart))
                .ToHashSet();
        }

        private static IEnumerable<string> GetUniqueWords(IEnumerable<string> words) =>
            words.Select(word => word.ToLower())
                .ToHashSet();
    }
}