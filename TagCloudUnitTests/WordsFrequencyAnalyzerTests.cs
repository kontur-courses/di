using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TagCloud.FrequencyAnalyzer;

namespace TagCloudUnitTests
{
    [TestFixture]
    internal class WordsFrequencyAnalyzerTests
    {
        private WordsFrequencyAnalyzer analyzer;

        [SetUp]
        public void Setup()
        {
            analyzer = new WordsFrequencyAnalyzer();
        }

        [Test]
        public void GetFrequencies_ReturnsCorrectSortedFrequencies()
        {
            var inputWords = new List<string>() { "one", "two", "two", "three", "three", "three", "four", "four", "four", "four" };

            var expectedFrequencies = new Dictionary<string, double>()
            {
               {"four", 0.4 },
               {"three", 0.3 },
               {"two", 0.2 },
               {"one", 0.1 },
            };

            var actualFrequencies = analyzer.GetFrequencies(inputWords);

            actualFrequencies.Keys.Should().BeEquivalentTo(expectedFrequencies.Keys);

            var expectedValues = expectedFrequencies.Values.ToList();
            var actualValues = actualFrequencies.Values.ToList();

            for (int i = 0; i < expectedValues.Count; i++)
                actualValues[i].Should().BeApproximately(expectedValues[i], 0.000001);
        }
    }
}
