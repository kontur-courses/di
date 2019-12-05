using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.WordProcessing.Filtering;
using TagsCloudContainer.WordProcessing.Filtering.CommandsExecuting;
using TagsCloudContainer.WordProcessing.Filtering.MyStem;

namespace TagsCloudContainerTests
{
    [TestFixture]
    public class ExcludingBoringWordsFilterTests
    {
        private ExcludingBoringWordsFilter filter;

        [SetUp]
        public void SetUp()
        {
            var pathToMyStemDirectory = Path.Combine(
                Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName,
                "TagsCloudContainer", "WordProcessing", "Filtering", "MyStem");
            filter = new ExcludingBoringWordsFilter(new CmdCommandExecutor(), new MyStemResultParser(),
                pathToMyStemDirectory);
        }


        [TestCaseSource(nameof(FilterWordsShouldReturnEmptyEnumerableWhenAllWordsAreBoringTestCases))]
        public void FilterWords_ShouldReturnEmptyEnumerable_WhenAllWordsAreBoring(List<string> words)
        {
            var filteredWords = filter.FilterWords(words);

            filteredWords.Should().BeEmpty();
        }

        private static IEnumerable FilterWordsShouldReturnEmptyEnumerableWhenAllWordsAreBoringTestCases
        {
            get
            {
                yield return new TestCaseData(new List<string> {"к", "из", "с", "в"}).SetName("all words are pretexts");
                yield return new TestCaseData(new List<string> {"он", "она", "моя", "его", "ее"}).SetName(
                    "all words are pronouns");
                yield return new TestCaseData(new List<string> {"и", "а", "но"}).SetName("all words are conjunctions");
                yield return new TestCaseData(new List<string> {"не", "вот", "даже", "ни"}).SetName(
                    "all words are particles");
            }
        }

        [Test]
        public void FieldWords_ShouldExcludeOnlyBoringWords()
        {
            var words = new[] {"я", "ты", "он", "она", "вместе", "целая", "страна"};

            var filteredWords = filter.FilterWords(words);

            var expectedFilteredWords = new[] {"вместе", "целая", "страна"};
            filteredWords.Should().BeEquivalentTo(expectedFilteredWords);
        }
    }
}