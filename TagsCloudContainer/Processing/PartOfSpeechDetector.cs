using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MyStemWrapper;

namespace TagsCloudContainer.Processing
{
    public static class PartOfSpeechDetector
    {
        private static readonly MyStem analyzer;
        private static readonly Dictionary<string, PartOfSpeech> abbreviations;

        static PartOfSpeechDetector()
        {
            analyzer = new MyStem
            {
                Parameters = "-i",
                PathToMyStem = @"Resources\mystem.exe"
            };

            abbreviations = new Dictionary<string, PartOfSpeech>
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
        }

        public static PartOfSpeech Detect(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentException("Слово не должно быть пустым или null");

            if (word.Contains(" "))
                throw new ArgumentException("Слово не должно содержать пробелов");

            var analysis = analyzer.Analysis(word);
            if (string.IsNullOrEmpty(analysis))
                return PartOfSpeech.Unknown;

            var match = Regex.Match(analysis, $@"^{word}{{[\w-]+=(\w+)");
            if (!match.Success)
                return PartOfSpeech.Unknown;

            var abbr = match.Groups[1].Value;
            return abbreviations.ContainsKey(abbr) ? abbreviations[abbr] : PartOfSpeech.Unknown;
        }
    }
}