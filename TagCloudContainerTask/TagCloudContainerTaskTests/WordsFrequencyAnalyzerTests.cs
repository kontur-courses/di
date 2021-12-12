using System.Collections.Generic;
using App.Implementation.Words.FrequencyAnalyzers;
using App.Infrastructure.Words.FrequencyAnalyzers;
using FluentAssertions;
using NUnit.Framework;

namespace TagCloudContainerTaskTests
{
    public class WordsFrequencyAnalyzerTests
    {
        private IFrequencyAnalyzer analyzer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            analyzer = new WordsFrequencyAnalyzer();
        }

        [Test]
        public void Should_CountCorrectFrequency()
        {
            var words = new List<string>
            {
                "Новости",
                "Новости",
                "Новости",
                "Новости",
                "Новости",
                "Погода",
                "Валюта",
                "Погода",
                "Технологии",
                "Технологии"
            };
            var expectedFrequency = new Dictionary<string, double>
            {
                { "Новости", 0.5 },
                { "Погода", 0.2 },
                { "Валюта", 0.1 },
                { "Технологии", 0.2 }
            };

            var actualFrequency = analyzer.AnalyzeWordsFrequency(words);

            actualFrequency.Should().BeEquivalentTo(expectedFrequency);
        }
    }
}