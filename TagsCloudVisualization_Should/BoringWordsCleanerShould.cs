using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Should
{
    public class BoringWordsCleanerShould
    {
        [Test]
        public void CleanWords_NotContainsBoringWords_TextWithBoringWords()
        {
            var words = new List<string>{"он", "пошел", "на", "встречу"};
            var expectedWords = new List<string>{"он", "пошел", "встречу"};
            var boringWords = new List<string>{"в","под","на"};
            var cleaner = new BoringWordsCleaner(boringWords.ToHashSet());

            var actualWords = cleaner.CleanWords(words);

            actualWords.Should().BeEquivalentTo(expectedWords);
        }
    }
}