using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using MyStemWrapper;

namespace TagsCloudContainer.Processing
{
    public static class PartOfSpeechDetector
    {
        private static readonly MyStem Analyzer;
        private static readonly Dictionary<string, PartOfSpeech> Abbreviations;
        private static readonly Regex WordRegex;

        static PartOfSpeechDetector()
        {
            Analyzer = new MyStem
            {
                Parameters = "-in",
                PathToMyStem = @"Resources\mystem.exe"
            };

            Abbreviations = new Dictionary<string, PartOfSpeech>
            {
                {"A", PartOfSpeech.Adjective},
                {"ADV", PartOfSpeech.Adverb},
                {"ADVPRO", PartOfSpeech.Pronoun},
                {"ANUM", PartOfSpeech.Numeral},
                {"APRO", PartOfSpeech.Pronoun},
                {"COM", PartOfSpeech.Union},
                {"CONJ", PartOfSpeech.Union},
                {"INTJ", PartOfSpeech.Interjection},
                {"NUM", PartOfSpeech.Numeral},
                {"PART", PartOfSpeech.Particle},
                {"PR", PartOfSpeech.Pretext},
                {"S", PartOfSpeech.Noun},
                {"SPRO", PartOfSpeech.Pronoun},
                {"V", PartOfSpeech.Verb}
            };

            WordRegex = new Regex(@"^([\w-]+?){[\w-]+?=(\w+)", RegexOptions.Multiline | RegexOptions.Compiled);
        }

        public static Dictionary<string, PartOfSpeech> Detect(IEnumerable<string> words)
        {
            var validWords = words.Distinct().Where(w => !string.IsNullOrEmpty(w) && !w.Contains(" "));

            var analysis = Analyzer.Analysis(string.Join(" ", validWords));
            var result = validWords.ToDictionary(w => w, _ => PartOfSpeech.Unknown);  // надо ли?

            var matches = WordRegex.Matches(analysis);
            foreach (Match match in matches)
            {
                var word = match.Groups[1].Value;
                var partOfSpeech = Abbreviations[match.Groups[2].Value];
                result[word] = partOfSpeech;
            }

            return result;
        }
    }
}