using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyStemAdapter
{
    public static class PartOfSpeechAbbreviations
    {
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private static readonly Dictionary<string, PartOfSpeech> Abbreviations = new Dictionary<string, PartOfSpeech>
        {
            ["??"] = PartOfSpeech.Unknown,
            ["A"] = PartOfSpeech.Adjective,
            ["ADV"] = PartOfSpeech.Adverb,
            ["ADVPRO"] = PartOfSpeech.AdverbPronoun,
            ["ANUM"] = PartOfSpeech.AdjectiveNumeric,
            ["APRO"] = PartOfSpeech.AdjectivePronoun,
            ["COM"] = PartOfSpeech.CompositePart,
            ["CONJ"] = PartOfSpeech.Conjecture,
            ["INTJ"] = PartOfSpeech.Interjection,
            ["NUM"] = PartOfSpeech.Numeric,
            ["PART"] = PartOfSpeech.Particle,
            ["PR"] = PartOfSpeech.Preposition,
            ["S"] = PartOfSpeech.Noun,
            ["SPRO"] = PartOfSpeech.Pronoun,
            ["V"] = PartOfSpeech.Verb
        };

        public static PartOfSpeech Parse(string s)
        {
            return Abbreviations.GetOrDefault(s);
        }
    }
}
