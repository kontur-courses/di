using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WordCloud.TextAnalyze;
using WordCloud.TextAnalyze.Words;

namespace TagCloudTests
{
    [TestFixture]
    public class WordsExtension_Tests
    {

        [Test]
        public void SortByEntries_BiggestInBeginning()
        {
            var words = new List<IWord>()
            {
                new Word("wordOne", 2),
                new Word("wordTwo", 4),
                new Word("wordThree", 8),
            };

            var sortedwords = words.SortByEntries();

            sortedwords.Should().BeInDescendingOrder(word => word.Entries);
        }

        [Test]
        public void CountEntries_StacksAllEntries()
        {
            IEnumerable<string> strings = new List<string>()
            {
                "wordOne",
                "wordOne",
                "wordOne",
                "wordTwo",
                "wordTwo",
                "wordTwo",
                "wordTwo",
            };

            var words = strings.CountEntries();
            var expectedwords = new List<IWord>()
            {
                new Word("wordOne", 3),
                new Word("wordTwo", 4),
            };
            words.Should().BeEquivalentTo(expectedwords);
        }

        [Test]
        public void Filter_NotGivenFilteredWords()
        {
            IEnumerable<string> strings = new List<string>()
            {
                "wordOne",
                "wordOne",
                "а",
                "wordTwo",
                "с",
                "для",
                "на",
            };
           var filteredStrings = strings.Filter(new CommonWords());

            filteredStrings.Should().NotContain("а")
                .And.NotContain("c")
                .And.NotContain("для")
                .And.NotContain("на");

        }
    }
}