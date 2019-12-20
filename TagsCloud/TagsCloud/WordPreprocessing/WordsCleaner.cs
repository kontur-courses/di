using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MyStemWrapper;
using Newtonsoft.Json.Linq;

namespace TagsCloud.WordPreprocessing
{
    public class WordsCleaner : IWordsProcessor
    {
        private readonly MyStem _stemmer = new MyStem();
        private readonly bool _infinitive;

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
            _stemmer.PathToMyStem = Path.Combine(System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location), "mystem.exe");
            _stemmer.Parameters = "-i --format json";
            _infinitive = infinitive;
        }

        public IEnumerable<string> ProcessWords(IEnumerable<string> words)
        {
            words = words.Select(w => w.ToLower());
            foreach (var word in words)
            {
                var wordData = _stemmer.Analysis(word);
                var partOfSpeech = GetPartOfSpeech(wordData);
                if (!boringPartsOfSpeech.Contains(partOfSpeech))
                    yield return _infinitive ? GetInfinitiveForm(wordData) : word;
            }
        }

        private PartOfSpeech GetPartOfSpeech(string jsonAnalysis)
        {
            var jsonArray = JArray.Parse(jsonAnalysis);
            if (!jsonAnalysis.Contains("gr")) return PartOfSpeech.Unknown;
            var designation = jsonArray[0]["analysis"][0]["gr"].ToString().Split(',', '=')[0];
            return partOfSpeechDenotation[designation];
        }

        private string GetInfinitiveForm(string jsonAnalysis)
        {
            var jsonArray = JArray.Parse(jsonAnalysis);
            return jsonArray[0]["analysis"][0]["lex"].ToString();
        }
    }
}