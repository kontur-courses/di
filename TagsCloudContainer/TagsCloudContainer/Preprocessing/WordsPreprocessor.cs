using System;
using System.Collections;
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
            this.wordInfoParser = wordInfoParser ?? throw new ArgumentNullException(nameof(wordInfoParser));
        }

        public IEnumerable<WordInfo> Process(IEnumerable<string> words)
        {
            if (words == null)
                throw new ArgumentNullException(nameof(words));

            var lowerWords = words.Select(word => word.ToLower());
            return wordInfoParser.ParseWords(lowerWords)
                .Where(wordInfo => !speechPartsToRemove.Contains(wordInfo.SpeechPart));
        }
    }
}