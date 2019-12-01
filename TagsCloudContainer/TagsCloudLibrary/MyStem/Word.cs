using System;
using System.Text.RegularExpressions;

namespace TagsCloudLibrary.MyStem
{
    public class Word
    {
        public enum PartOfSpeech
        {
            Adjective,
            Adverb,
            AdverbPronoun,
            AdjectiveNumeral,
            AdjectivePronoun,
            Composite,
            Conjunction,
            Interjection,
            Numeral,
            Particle,
            Preposition,
            Noun,
            NounPronoun,
            Verb
        }

        public class WordGrammar
        {
            public PartOfSpeech PartOfSpeech;
            public string InitialForm;
        }

        private readonly Regex WordAndLemmaRegex = new Regex(@"^([А-Яа-я\w-]+)\{([А-Яа-я\w=,\|\-\d?]+)\}$");

        public string InitialString { get; }
        public WordGrammar Grammar { get; }

        public Word(string myStemConclusion)
        {
            try
            {
                var match = WordAndLemmaRegex.Match(myStemConclusion);
                var lemma = match.Groups[2].Value;
                var possibilities = lemma.Split('|');
                var possibility = possibilities[0].Split('=');
                var initialForm = possibility[0];
                var grammarInfo = possibility[1];
                var partOfSpeechInfo = grammarInfo.Split(',')[0];

                InitialString = match.Groups[1].Value;
                Grammar = new WordGrammar
                {
                    InitialForm = initialForm,
                    PartOfSpeech = PartOfSpeechFromMystem(partOfSpeechInfo)
                };
            }
            catch (Exception e)
            {
                throw new FormatException("Wrong mystem conclusion was given", e);
            }
        }

        public PartOfSpeech PartOfSpeechFromMystem(string partOfSpeechInfo)
        {
            switch (partOfSpeechInfo)
            {
                case "A":
                    return PartOfSpeech.Adjective;
                case "ADV":
                    return PartOfSpeech.Adverb;
                case "ADVPRO":
                    return PartOfSpeech.AdverbPronoun;
                case "ANUM":
                    return PartOfSpeech.AdjectiveNumeral;
                case "APRO":
                    return PartOfSpeech.AdjectivePronoun;
                case "COM":
                    return PartOfSpeech.Composite;
                case "CONJ":
                    return PartOfSpeech.Conjunction;
                case "INTJ":
                    return PartOfSpeech.Interjection;
                case "NUM":
                    return PartOfSpeech.Numeral;
                case "PART":
                    return PartOfSpeech.Particle;
                case "PR":
                    return PartOfSpeech.Preposition;
                case "S":
                    return PartOfSpeech.Noun;
                case "SPRO":
                    return PartOfSpeech.NounPronoun;
                case "V":
                    return PartOfSpeech.Verb;
                default:
                    throw new FormatException("Wrong part of speech info");
            }
        }
    }
}