using System.Collections.Generic;
using System.Linq;
using DeepMorphy;
using DeepMorphy.Model;

namespace TagsCloudContainer.WordsPreparator
{
    public record RussianWordsPreparator : IWordsPreparator
    {
        private const string SpeechPartKey = "чр";
        
        private static readonly IReadOnlyDictionary<string, SpeechPart> SpeechPartsAdapter = new Dictionary<string, SpeechPart>
        {
            ["сущ"] = SpeechPart.Noun,
            ["прил"] = SpeechPart.Adjective,
            ["кр_прил"] = SpeechPart.Adjective,
            ["гл"] = SpeechPart.Verb,
            ["инф_гл"] = SpeechPart.Verb,
            ["нареч"] = SpeechPart.Adverbs,
            ["мест"] = SpeechPart.Pronoun,
            ["числ"] = SpeechPart.Num
        };
        
        private readonly MorphAnalyzer analyzer;
        

        public RussianWordsPreparator(MorphAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        public IEnumerable<WordInfo> Prepare(IEnumerable<string> words)
        {
            var preparedWords = words
                .Select(ToLowerAndTrim)
                .SelectMany(s => s.Split());
            return CreateWordInfo(analyzer.Parse(preparedWords));
        }

        private IEnumerable<WordInfo> CreateWordInfo(IEnumerable<MorphInfo> parsedWords)
        {
            return parsedWords
                .Select(morphInfo => morphInfo.BestTag)
                .Select(tag => new WordInfo(tag.Lemma, IdentifySpeechPart(tag)));
        }

        private string ToLowerAndTrim(string line)
        {
            return line.Trim().ToLower();
        }

        private SpeechPart IdentifySpeechPart(Tag tag)
        {
            var speechPart = tag.GramsDic[SpeechPartKey];
            return SpeechPartsAdapter.ContainsKey(speechPart)
                ? SpeechPartsAdapter[speechPart]
                : SpeechPart.Unknown;
        }
    }
}