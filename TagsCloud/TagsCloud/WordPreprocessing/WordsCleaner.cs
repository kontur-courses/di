using System;
using System.Collections.Generic;
using MyStemWrapper;
using Newtonsoft.Json.Linq;

namespace TagsCloud.WordPreprocessing
{
    public class WordsCleaner : IWordsProcessor
    {
        private MyStem stemmer = new MyStem();
        private bool _infinitive;

        private readonly Dictionary<string, PartOfSpeech> partOfSpeechDenotation =
            new Dictionary<string, PartOfSpeech>
            {
                {"A", PartOfSpeech.Adjective},
                {"ADV", PartOfSpeech.Adverb},
                {"ADVPRO", PartOfSpeech.PronominalAdverb},
                {"ANUM", PartOfSpeech.NumeralAdjective},
                {"APRO", PartOfSpeech.PronounAdjective},
                {"COM", PartOfSpeech.CompositePart},
                {"CONJ", PartOfSpeech.Conjunction},
                {"INTJ", PartOfSpeech.Interjection},
                {"NUM", PartOfSpeech.Numeral},
                {"PART", PartOfSpeech.Particle},
                {"PR", PartOfSpeech.Pretext},
                {"S", PartOfSpeech.Noun},
                {"SPRO", PartOfSpeech.PronounNoun},
                {"V", PartOfSpeech.Verb}
            };

        private readonly HashSet<PartOfSpeech> boringPartsOfSpeech = new HashSet<PartOfSpeech>
        {
            PartOfSpeech.Particle,
            PartOfSpeech.Conjunction,
            PartOfSpeech.Pretext,
            PartOfSpeech.PronounNoun,
            PartOfSpeech.PronounAdjective,
            PartOfSpeech.Unknown
        };

        public WordsCleaner(bool infinitive)
        {
            stemmer.PathToMyStem = @"D:\Универ\Шпора\mystem.exe";
            stemmer.Parameters = "-i --format json";
            _infinitive = infinitive;
        }

        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                var partOfSpeech = GetPartOfSpeech(word);
                if (!boringPartsOfSpeech.Contains(partOfSpeech))
                    yield return _infinitive ? GetInfinitiveForm(word) : word;
            }
        }

        private PartOfSpeech GetPartOfSpeech(string word)
        {
            var jsonAnalysis = stemmer.Analysis(word);
            var jsonArray = JArray.Parse(jsonAnalysis);
            if (!jsonAnalysis.Contains("gr")) return PartOfSpeech.Unknown;
            var designation = jsonArray[0]["analysis"][0]["gr"].ToString().Split(',', '=')[0];
            return partOfSpeechDenotation[designation];
        }

        private string GetInfinitiveForm(string word)
        {
            var jsonAnalysis = stemmer.Analysis(word);
            var jsonArray = JArray.Parse(jsonAnalysis);
            return jsonArray[0]["analysis"][0]["lex"].ToString();
        }
    }
}