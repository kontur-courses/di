using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Infrastructure.Analyzer;
using TagsCloudVisualization.Infrastructure.Parsers;
using TagsCloudVisualization.Settings;

namespace TagsCloudContainerTests
{

    public class AnalyzerShould
    {
        private  Analyzer analyzer;
        private  AnalyzerSettings settings;
        private readonly string[] boring = { "а", "перед", "что", "и", "я", "он", "ты", "они" };
        private readonly string[] verbs = { "удаляет", "смотрит", "наблюдает" };
        private readonly string[] nouns = { "СОЛОМА, МУДРОСТЬ" };


        [SetUp]
        public void SetUp()
        {
            settings = new AnalyzerSettings
            {
                ExcludedParts = new List<PartSpeech>(),
            };
            analyzer = new Analyzer(settings);
        }


        [Test]
        public void ChangeCase()
        {
            var fake = A.Fake<IParser>();
            A.CallTo(() => fake.WordParse())
                .Returns(nouns);

            analyzer.SetParser(fake);
            var result = analyzer.CreateFrequencyDictionary().Keys;

            result.Should().BeEquivalentTo(nouns.Select(s => s.ToLower()));
        }

        [Test]
        public void CreateFrequencyDictionary()
        {
            var fake = A.Fake<IParser>();
            var words = new[] { "Привет", "привет", "ПрИвЕт" };
            A.CallTo(() => fake.WordParse())
                .Returns(words);
            analyzer.SetParser(fake);

            var result = analyzer.CreateFrequencyDictionary();

            result.Keys.Should().BeEquivalentTo("привет");
            result.Values.First().Should().Be(3);
        }

        [Test]
        public void ExcludeBoringWords()
        {
            var fake = A.Fake<IParser>();
            A.CallTo(() => fake.WordParse())
                .Returns(boring);
            analyzer.SetParser(fake);

            settings.ExcludedParts = new List<PartSpeech>
            {
                PartSpeech.Preposition,
                PartSpeech.Pronoun,
                PartSpeech.Interjection,
                PartSpeech.Particle,
                PartSpeech.Unknown
            };

            var result = analyzer.CreateFrequencyDictionary();

            result.Keys.Should().BeEmpty();
        }

        [Test]
        public void ChoosePartsSpeech()
        {
            var words = nouns.Concat(verbs);
            var fake = A.Fake<IParser>();
            A.CallTo(() => fake.WordParse())
                .Returns(words);
            analyzer.SetParser(fake);
            settings.SelectedParts.Add(PartSpeech.Noun);

            var result = analyzer.CreateFrequencyDictionary();

            result.Keys.Should().BeEquivalentTo(nouns.Select(s => s.ToLower()));
        }

        [Test]
        public void Lemmatization()
        {
            var fake = A.Fake<IParser>();
            A.CallTo(() => fake.WordParse())
                .Returns(verbs);
            analyzer.SetParser(fake);
            settings.Lemmatization = true;

            var result = analyzer.CreateFrequencyDictionary();

            result.Keys.Should().BeEquivalentTo("удалять", "смотреть", "наблюдать");
        }
    }
}
