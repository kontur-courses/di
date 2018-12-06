using System.Collections.Generic;
using System.Linq;
using MyStemAdapter;

namespace TagsCloudContainer
{
    public class WordPreprocessor
    {
        private FilterHashSet<string> WordsBlackList { get; } = new FilterHashSet<string>(FilterType.BlackList);
        private readonly FilterHashSet<PartOfSpeech> partOfSpeechFilter;
        private readonly MyStemAdapter.MyStemAdapter myStemAdapter = new MyStemAdapter.MyStemAdapter();

        private static readonly PartOfSpeech[] DefaultPartOfSpeechBlacklist =
        {
            PartOfSpeech.Particle,
            PartOfSpeech.Preposition,
            PartOfSpeech.Conjecture,
            PartOfSpeech.Unknown
        };

        public WordPreprocessor()
        {
            partOfSpeechFilter =
                new FilterHashSet<PartOfSpeech>(FilterType.BlackList, DefaultPartOfSpeechBlacklist);
        }

        public WordPreprocessor(FilterHashSet<PartOfSpeech> partOfSpeechFilter)
        {
            this.partOfSpeechFilter = partOfSpeechFilter;
        }

        public IEnumerable<string> PreprocessWords(IEnumerable<string> words)
        {
            return words
                .Select(word => myStemAdapter.GetWordInfo(word))
                .Where(wordInfo => WordsBlackList.PassesFilter(wordInfo.Stem) &&
                                   partOfSpeechFilter.PassesFilter(wordInfo.PartOfSpeech))
                .Select(wordInfo => wordInfo.Stem);
        }
    }
}
