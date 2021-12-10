using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using TagsCloudContainer.WordsPreparator;

namespace TagsCloudContainer.WordsFilter
{
    public record WordsFilter : IWordsFilter
    {
        private ImmutableHashSet<SpeechPart> excludedSpeechParts = ImmutableHashSet<SpeechPart>.Empty;
        private Predicate<WordInfo> isBoring = wi => wi.Lemma.Length <= 2;


        public IEnumerable<WordInfo> Filter(IEnumerable<WordInfo> words)
        {
            return words
                .Where(word => !excludedSpeechParts.Contains(word.SpeechPart) && !isBoring(word))
                .Select(word => word.Lemma);
        }

        public WordsFilter Excluding(SpeechPart speechPart)
        {
            return new WordsFilter {excludedSpeechParts = excludedSpeechParts.Add(speechPart)};
        }

        public WordsFilter Excluding(Predicate<WordInfo> boringWords)
        {
            return new WordsFilter {isBoring = boringWords};
        }
    }
}