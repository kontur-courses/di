using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DeepMorphy;
using DeepMorphy.Model;
using TagsCloudVisualization.Enums;

namespace TagsCloudVisualization
{
    public static class Preprocessor
    {
        private static readonly Regex Pattern = new Regex(@"([^\d\W]+)", RegexOptions.Compiled);
        
        private static readonly Dictionary<string, PartSpeech> PartSpeechDictionary = new Dictionary<string, PartSpeech>()
        {
            { "сущ", PartSpeech.Noun },
            { "гл", PartSpeech.Verb },
            { "инф_гл", PartSpeech.InfinitiveVerb },
            { "прич", PartSpeech.Participle },
            { "кр_прич", PartSpeech.ShortParticiple },
            { "деепр", PartSpeech.AdverbialParticiple },
            { "нареч", PartSpeech.Adverb },
            { "мест", PartSpeech.Pronoun },
            { "прил", PartSpeech.Adjective },
            { "кр_прил", PartSpeech.ShortAdjective },
            { "неизв", PartSpeech.Unknown },
        };

        public static IEnumerable<string> Process(string[] textLines, PartSpeech flag = PartSpeech.None, IEnumerable<string> unnecessaryWords = null)
        {
            unnecessaryWords ??= Array.Empty<string>();
            
            var morph = new MorphAnalyzer(withLemmatization: true);
            var words = new List<string>();

            foreach (var line in textLines)
            {
                var lineWords = Pattern.Matches(line).Select(x => x.Value);
                
                var morphInfos = morph.Parse(lineWords);

                words.AddRange(MorphAnalysisLineWords(morphInfos, flag, unnecessaryWords));
            }

            return words;
        }

        private static IEnumerable<string> MorphAnalysisLineWords(IEnumerable<MorphInfo> morphInfos, 
            PartSpeech flag, IEnumerable<string> unnecessaryWords)
        {
            var words = new List<string>();
            
            foreach (var morphInfo in morphInfos)
            {
                var bestTag = morphInfo.BestTag;

                var partSpeech = bestTag.GramsDic["чр"];

                if (!PartSpeechDictionary.ContainsKey(partSpeech))
                    continue;

                if (!flag.HasFlag(PartSpeechDictionary[partSpeech]))
                    continue;

                var word = bestTag.HasLemma ? bestTag.Lemma : morphInfo.Text;

                if (unnecessaryWords.Contains(word)) 
                    continue;
                
                words.Add(word);
            }

            return words;
        }
    }
}