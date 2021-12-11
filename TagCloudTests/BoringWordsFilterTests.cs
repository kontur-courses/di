using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Analyzers;

namespace TagCloudTests
{
    public class BoringWordsFilterTests
    {
        private BoringWordsFilter filter;

        [SetUp]
        public void SetUp()
        {
            filter = new BoringWordsFilter();
        }

        [Test]
        public void Analyze_ShouldSkipBoringWords()
        {
            var text = new[] { "I", "met", "you", "a", "long", "time", "ago" };
            var boringWords = new HashSet<string> { "I", "you", "a", "ago" };
            filter.AddWords(boringWords);

            var analyzedWords = filter.Analyze(text);

            analyzedWords.Should().BeEquivalentTo("met", "long", "time");
        }
    }
}
