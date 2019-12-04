using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TagsCloudContainer.WordProcessing.Filtering.PartsOfSpeechQualifying.MyStem
{
    public class MyStemResultParser
    {
        public IEnumerable<(string, PartOfSpeech)> GetPartsOfSpeechByResultOfNiCommand(string myStemResult,
            IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                var partOfSpeechRegex = new Regex(word + @"{.+?=(\w+)[,|=]");
                var match = partOfSpeechRegex.Match(myStemResult);
                var matchGroups = match.Groups;
                if (matchGroups.Count < 2)
                {
                    throw new InvalidOperationException($"{nameof(myStemResult)} does not contain result for {word}");
                }

                var wordPartOfSpeech = GetPartOfSpeechByName(matchGroups[1].Value);
                yield return (word, wordPartOfSpeech);
            }
        }

        private PartOfSpeech GetPartOfSpeechByName(string partOfSpeechName)
        {
            switch (partOfSpeechName)
            {
                case "A":
                    return PartOfSpeech.Adjective;
                case "ADV":
                    return PartOfSpeech.Adverb;
                case "ADVPRO":
                    return PartOfSpeech.Pronoun;
                case "ANUM":
                    return PartOfSpeech.Adjective;
                case "APRO":
                    return PartOfSpeech.Pronoun;
                case "COM":
                    return PartOfSpeech.Compound;
                case "CONJ":
                    return PartOfSpeech.Conjunction;
                case "INTJ":
                    return PartOfSpeech.Interjection;
                case "NUM":
                    return PartOfSpeech.Numeral;
                case "PART":
                    return PartOfSpeech.Particle;
                case "PR":
                    return PartOfSpeech.Pretext;
                case "S":
                    return PartOfSpeech.Noun;
                case "SPRO":
                    return PartOfSpeech.Pronoun;
                case "V":
                    return PartOfSpeech.Verb;
                default:
                    throw new ArgumentException($"incorrect part of speech name {partOfSpeechName}");
            }
        }
    }
}