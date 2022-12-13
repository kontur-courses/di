using System.Collections.Generic;
using System.Linq;
using DeepMorphy;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Infrastructure.Analyzer
{
    public class Analyzer : IAnalyzer
    {
        private readonly Dictionary<PartSpeech, string> converter = new()
        {
            [PartSpeech.Noun] = "сущ",
            [PartSpeech.Adjective] = "прил",
            [PartSpeech.Verb] = "гл",
            [PartSpeech.Infinitive] = "инф_гл",
            [PartSpeech.Participle] = "прич",
            [PartSpeech.Gerund] = "деепр",
            [PartSpeech.Adverb] = "нареч",
            [PartSpeech.Conjunction] = "союз",
            [PartSpeech.Pronoun] = "мест",
            [PartSpeech.Particle] = "част",
            [PartSpeech.Preposition] = "предл",
            [PartSpeech.Interjection] = "межд",
            [PartSpeech.Unknown] = "неизв"
        };

        private readonly AnalyzerSettings settings;

        public Analyzer(AnalyzerSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<IWeightedWord> CreateWordFrequenciesSequence(IEnumerable<string> words)
        {
            var result = new Dictionary<string, int>();

            var remainingWords = Analyze(words);

            foreach (var word in remainingWords)
            {
                if (!result.ContainsKey(word))
                    result[word] = 0;
                result[word]++;
            }

            foreach (var pair in result) yield return new WeightedWord { Weight = pair.Value, Word = pair.Key };
        }

        private IEnumerable<string> Analyze(IEnumerable<string> words)
        {
            var analyzer = new MorphAnalyzer(settings.Lemmatization, withTrimAndLower: true);
            var excludedTags = settings.ExcludedParts
                .Select(p => converter[p])
                .ToHashSet();
            var selectedTags = settings.SelectedParts
                .Select(p => converter[p])
                .ToHashSet();

            var parsedWords = analyzer
                .Parse(words.Where(s => s.Length > 0))
                .Where(m => m.Tags.All(t => !excludedTags.Contains(t["чр"]))
                            && (selectedTags.Count == 0 ||
                                selectedTags.Contains(m.BestTag["чр"])))
                .Where(m => !settings.Lemmatization || m.BestTag.HasLemma)
                .Select(m => settings.Lemmatization ? m.BestTag.Lemma : m.Text);

            foreach (var word in parsedWords)
                yield return word;
        }
    }
}