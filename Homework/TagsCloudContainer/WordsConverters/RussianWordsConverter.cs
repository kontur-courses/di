using System.Collections.Generic;
using System.Linq;
using DeepMorphy;
using DeepMorphy.Model;

namespace TagsCloudContainer.WordsConverters
{
    public record RussianWordsConverter : IWordsConverter
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
        

        public RussianWordsConverter(MorphAnalyzer analyzer)
        {
            this.analyzer = analyzer;
        }

        public ICollection<WordInfo> Convert(IEnumerable<string> words)
        {
            var preparedWords = words
                .Select(ToLowerAndTrim)
                .SelectMany(s => s.Split());
            return CreateWordInfo(analyzer.Parse(preparedWords))
                .ToArray();
        }

        private IEnumerable<WordInfo> CreateWordInfo(IEnumerable<MorphInfo> parsedWords)
        {
            return parsedWords
                .Select(morphInfo => new WordInfo(morphInfo.BestTag.Lemma ?? morphInfo.Text,
                    IdentifySpeechPart(morphInfo.BestTag)));
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