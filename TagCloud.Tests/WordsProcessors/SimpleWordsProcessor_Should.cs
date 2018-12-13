using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Core.Util;
using TagCloud.Core.WordsParsing.WordsProcessing;
using TagCloud.Core.WordsParsing.WordsProcessing.WordsProcessingUtilities;

namespace TagCloud.Tests.WordsProcessors
{
    [TestFixture]
    public class SimpleWordsProcessor_Should
    {
        private SimpleWordsProcessor wordsProcessor;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            wordsProcessor = new SimpleWordsProcessor(new IWordsProcessingUtility[0]);
        }

        [Test]
        public void MergeTwoTheSameUnhandledWordsIntoOneWord()
        {
            var unhandledWords = new List<string> {"w1", "w1"};
            var expectedWords = new List<TagStat> {new TagStat("w1", 2)};

            var res = wordsProcessor.Process(unhandledWords);

            res.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void BeCaseSensitiveByDefault()
        {
            var unhandledWords = new List<string> { "word", "wOrD" };
            var expectedWords = unhandledWords.Select(unhandledWord => new TagStat(unhandledWord, 1));

            var res = wordsProcessor.Process(unhandledWords);

            res.Should().BeEquivalentTo(expectedWords);
        }

        [Test]
        public void ReturnEmptyList_WhenEmptyListOfUnhandledWordsGiven()
        {
            var res = wordsProcessor.Process(new List<string>());
            res.Should().NotBeNull();
            res.Count().Should().Be(0);
        }

        [Test]
        public void ThrowArgumentException_WhenGivenNull()
        {
            Action action = () => wordsProcessor.Process(null);
            action.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ExcludeBoringWords_WhenTheyGiven()
        {
            const string boringWord = "boring_word";
            const string interestingWord = "interesting_word";
            var unhandledWords = new List<string> { interestingWord, boringWord };
            var expectedWords = new List<TagStat> { new TagStat(interestingWord, 1) };

            var res = wordsProcessor.Process(unhandledWords, new HashSet<string>{boringWord});

            res.Should().BeEquivalentTo(expectedWords);
        }
    }
}