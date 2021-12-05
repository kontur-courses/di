using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Analyzers;

namespace TagCloudTests
{
    public class TextAnalyzerTests
    {
        private ITextAnalyzer textAnalyzer;

        [SetUp]
        public void SetUp()
        {
            textAnalyzer = new TextAnalyzer();
        }

        [Test]
        public void Analyze_ShouldSkipBoringWords()
        {
            var text = new[] {"I", "met", "you", "a", "long", "time", "ago"};
            var boringWords = new HashSet<string> {"I", "you", "a", "ago"};

            var analyzedWords = textAnalyzer.Analyze(text, boringWords);

            analyzedWords.Should().BeEquivalentTo("met", "long", "time");
        }

        [Test]
        public void Analyze_ShouldConvertWordsToLowerCase()
        {
            var text = new[] { "I", "MET", "yOu", "A", "lOnG", "TiMe", "aGO" };
            var boringWords = new HashSet<string>();

            var analyzedWords = textAnalyzer.Analyze(text, boringWords);

            analyzedWords.Should().BeEquivalentTo("i", "met", "you", "a", "long", "time", "ago");
        }
    }
}
