using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using DeepMorphy;
using DeepMorphy.Model;

namespace TagsCloudContainer.WordsPreparator
{
    public record RussianWordsPreparator : IWordsPreparator
    {
        private readonly MorphAnalyzer analyzer;
        private static readonly IReadOnlyDictionary<string, SpeechPart> speechPartsAdapter;

        static RussianWordsPreparator()
        {
            speechPartsAdapter = new Dictionary<string, SpeechPart>()
            {
                ["сущ"] = SpeechPart.Noun,
                ["прил"] = SpeechPart.Adjective,
                ["кр_прил"] = SpeechPart.Adjective,
                ["гл"] = SpeechPart.Verb,
                ["инф_гл"] = SpeechPart.Verb,
                ["нареч"] = SpeechPart.Adverbs,
                ["мест"] = SpeechPart.Pronoun,
                ["числ"] = SpeechPart.Num,
            };
        }

        public RussianWordsPreparator(MorphAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }
        
        public IEnumerable<WordInfo> Prepare(IEnumerable<string> words)
        {
            var preparedWords = words
                .Select(w => ToLowerAndTrim(w))
                .Select(w => w.Split()[0]);
            return CreateWordInfo(analyzer.Parse(preparedWords));
        }

        private IEnumerable<WordInfo> CreateWordInfo(IEnumerable<MorphInfo> parsedWords)
        {
            return parsedWords
                .Select(mi => mi.BestTag)
                .Select(tag => new WordInfo(tag.Lemma, IdentifySpeechPart(tag)));
        }

        private string ToLowerAndTrim(string line) => line.Trim().ToLower();

        private SpeechPart IdentifySpeechPart(Tag tag)
        {
            var speechPart = tag.GramsDic["чр"];
            return speechPartsAdapter.ContainsKey(speechPart)
                ? speechPartsAdapter[speechPart]
                : SpeechPart.Unknown;
        }

    }
}