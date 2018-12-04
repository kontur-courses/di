using System;
using System.Collections.Generic;
using MyStemWrapper;
using Newtonsoft.Json.Linq;


namespace TagsCloud.WordPrework
{
    public class WordAnalyzer
    {
        private readonly Dictionary<string, PartOfSpeech> partsOfSpeechDesignations =
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
            PartOfSpeech.Particle, PartOfSpeech.Conjunction,  PartOfSpeech.Pretext,  PartOfSpeech.PronominalAdverb,
            PartOfSpeech.PronounNoun, PartOfSpeech.PronounAdjective
        };

        private readonly IEnumerable<string> words;
        private readonly Dictionary<string, int> WordsFrequency = new Dictionary<string, int>();
        private MyStem stemmer = new MyStem();

        public WordAnalyzer(IWordsGetter wordsGetter,bool useInfinitiveForm = false)
        {
            stemmer.Parameters = "-i --format json";
            words = wordsGetter.GetWords();
            foreach (var word in words)
            {
                var wordForm = useInfinitiveForm ? GetInfinitiveForm(word) : word.ToLower();
                if (WordsFrequency.ContainsKey(wordForm))
                    WordsFrequency[wordForm] += 1;
                else
                    WordsFrequency[wordForm] = 1;
            }
        }

        public Dictionary<string, int> GetWordFrequency()
        {
            var result = new Dictionary<string, int>();
            foreach (var item in WordsFrequency)
            {
                var partOfSpeech = GetPartOfSpeech(item.Key);
                if (!boringPartsOfSpeech.Contains(partOfSpeech))
                    result[item.Key] = item.Value;
            }

            return result;
        }

        public Dictionary<string, int> GetWordFrequency(params PartOfSpeech[] partsOfSpeech)
        {
            var result = new Dictionary<string, int>();
            var neededParts = new HashSet<PartOfSpeech>(partsOfSpeech);
            foreach (var item in WordsFrequency)
            {
                var partOfSpeech = GetPartOfSpeech(item.Key);
                if (neededParts.Contains(partOfSpeech))
                    result[item.Key] = item.Value;
            }
            return result;
        }

        private string GetInfinitiveForm(string word)
        {
            var jsonAnalysis = stemmer.Analysis(word);
            var jsonArray = JArray.Parse(jsonAnalysis);
            return jsonArray[0]["analysis"][0]["lex"].ToString();
        }

        private PartOfSpeech GetPartOfSpeech(string word)
        {
            var jsonAnalysis = stemmer.Analysis(word);
            var jsonArray = JArray.Parse(jsonAnalysis);
            var designation = jsonArray[0]["analysis"][0]["gr"].ToString().Split(',', '=')[0];
            return partsOfSpeechDesignations[designation];
        }
    }
}
