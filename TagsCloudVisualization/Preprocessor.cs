using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using DeepMorphy;
using TagsCloudVisualization.Enums;

namespace TagsCloudVisualization
{
    public static class Preprocessor
    {
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

        public static IEnumerable<string> Process(string path, PartSpeech flag = PartSpeech.None, IEnumerable<string> unnecessaryWords = null)
        {
            unnecessaryWords ??= Array.Empty<string>();
            
            var morph = new MorphAnalyzer(withLemmatization: true);
            var words = new List<string>();
            var lines = File.ReadAllLines(path);

            foreach (var line in lines)
            {
                var r2= new Regex(@"([а-яёА-ЯЁa-zA-Z]*)");
                var lineWords = r2.Matches(line).Select(x => x.Value).Where(x => !string.IsNullOrEmpty(x));
                
                var morhsInfo = morph.Parse(lineWords);
 
                foreach (var morphInfo in morhsInfo)
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
            }

            return words;
        }
    }
}