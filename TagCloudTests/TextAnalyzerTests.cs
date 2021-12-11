using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Analyzers;

namespace TagCloudTests
{
    public class TextAnalyzerTests
    {
        private ITextAnalyzer textAnalyzer;
        private BoringWordsFilter filter;

        [SetUp]
        public void SetUp()
        {
            filter = new BoringWordsFilter();
            textAnalyzer = new TextAnalyzer(new []{filter},
                new []{new WordsToLowerConverter()},
                new FrequencyAnalyzer());
        }

        [Test]
        public void Analyze_ShouldReturnCountedWords()
        {
            var text = new[] { "I", "met", "you", "a", "long", "time", "ago" };
            var boringWords = new HashSet<string> { "i", "you", "a", "ago" };
            filter.AddWords(boringWords);

            var analyzedWords = textAnalyzer.Analyze(text);

            analyzedWords.Keys.Should().HaveCount(3);
        }
    }
}
