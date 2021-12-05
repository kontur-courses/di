using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Analyzers;

namespace TagCloudTests
{
    public class FrequencyAnalyzerTests
    {
        private IFrequencyAnalyzer frequencyAnalyzer;

        [SetUp]
        public void SetUp()
        {
            frequencyAnalyzer = new FrequencyAnalyzer();
        }

        [Test]
        public void Analyze_ShouldCountWords()
        {
            var text = new[] { "a", "a", "a", "a", "a", "b", "b", "b"};

            var analyzedWords = frequencyAnalyzer.Analyze(text);

            analyzedWords["a"].Should().Be(5);
            analyzedWords["b"].Should().Be(3);
        }

        [Test]
        public void Analyze_ShouldReturnsEmptyDictionary_WhenNoWords()
        {
            var text = new List<string>();

            var analyzedWords = frequencyAnalyzer.Analyze(text);

            analyzedWords.Count.Should().Be(0);
        }
    }
}
