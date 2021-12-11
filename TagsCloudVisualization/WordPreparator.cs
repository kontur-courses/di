#region

using System.Collections.Generic;
using System.Linq;
using DeepMorphy;
using DeepMorphy.Model;
using TagsCloudVisualization.Enums;
using TagsCloudVisualization.Interfaces;

#endregion

namespace TagsCloudVisualization
{
    public class WordPreparator : IWordPreparator
    {
        private readonly HashSet<string> excludedSpeechParts = new();
        private readonly MorphAnalyzer morphAnalyzer;

        private readonly Dictionary<SpeechPart, string> speechPartsDictionary = new()
        {
            { SpeechPart.Noun, "сущ" },
            { SpeechPart.Adjective, "прил" },
            { SpeechPart.Verb, "гл" },
            { SpeechPart.AdverbialParticiple, "деепр" },
            { SpeechPart.Preposition, "предл" }
        };

        public WordPreparator(MorphAnalyzer morphAnalyzer)
        {
            this.morphAnalyzer = morphAnalyzer;
        }

        public IEnumerable<string> GetPreparedWords(IEnumerable<string> unpreparedWords)
        {
            var words = morphAnalyzer.Parse(unpreparedWords).ToArray();

            return words.Where(info => !IsMorphInfoExcluded(info)).Select(t => t.BestTag.Lemma).ToList();
        }

        public WordPreparator Exclude(IEnumerable<SpeechPart> speechParts)
        {
            foreach (var speechPart in speechParts)
                excludedSpeechParts.Add(speechPartsDictionary[speechPart]);

            return this;
        }

        public WordPreparator Exclude(SpeechPart speechPart)
        {
            excludedSpeechParts.Add(speechPartsDictionary[speechPart]);

            return this;
        }

        private bool IsMorphInfoExcluded(MorphInfo morphInfo)
        {
            return excludedSpeechParts.Contains(morphInfo.BestTag.GramsDic["чр"]);
        }
    }
}