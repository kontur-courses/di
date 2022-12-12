using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Settings;

namespace TagsCloudContainerTests
{
    public class AnalyzerShould
    {
        private readonly string[] boring = { "а", "перед", "что", "и", "я", "он", "ты", "они" };
        private readonly string[] nouns = { "СОЛОМА, МУДРОСТЬ" };
        private readonly string[] verbs = { "удаляет", "смотрит", "наблюдает" };
        private Analyzer analyzer;
        private AnalyzerSettings settings;


        [SetUp]
        public void SetUp()
        {
            settings = new AnalyzerSettings
            {
                ExcludedParts = new List<PartSpeech>()
            };
            analyzer = new Analyzer(settings);
        }

        [Test]
        public void ChangeCase()
        {
            var result = analyzer.CreateWordFrequenciesSequence(nouns);

            result.Select(w => w.Word).Should()
                .BeEquivalentTo(nouns.Select(s => s.ToLower()));
        }

        [Test]
        public void CreateFrequencyDictionary()
        {
            var words = new[] { "Привет", "привет", "ПрИвЕт" };

            var result = analyzer.CreateWordFrequenciesSequence(words);

            result.First().Should().BeEquivalentTo(new WeightedWord { Weight = 3, Word = "привет" });
        }

        [Test]
        public void ExcludeBoringWords()
        {
            settings.ExcludedParts = new List<PartSpeech>
            {
                PartSpeech.Preposition,
                PartSpeech.Pronoun,
                PartSpeech.Interjection,
                PartSpeech.Particle,
                PartSpeech.Unknown
            };

            var result = analyzer.CreateWordFrequenciesSequence(boring);

            result.Should().BeEmpty();
        }

        [Test]
        public void ChoosePartsSpeech()
        {
            var words = nouns.Concat(verbs);

            settings.SelectedParts.Add(PartSpeech.Noun);

            var result = analyzer.CreateWordFrequenciesSequence(words);

            result.Select(w => w.Word)
                .Should()
                .BeEquivalentTo(nouns.Select(s => s.ToLower()));
        }

        [Test]
        public void Lemmatization()
        {
            settings.Lemmatization = true;

            var result = analyzer.CreateWordFrequenciesSequence(verbs);

            result.Select(w => w.Word)
                .Should().BeEquivalentTo("удалять", "смотреть", "наблюдать");
        }
    }
}