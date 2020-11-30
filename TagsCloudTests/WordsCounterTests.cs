using System.Collections.Generic;
using NUnit.Framework;
using FluentAssertions;
using TagsCloud.App;

namespace TagsCloudTests
{
    public class WordsCounterTests
    {

        [Test]
        public void WordsCounter_ShouldReturnEmptyCollection_WithEmptyArray()
        {
            var wordsCounter = new WordsCounter();
            var wordsCounts = wordsCounter.CountWords(new string[0]);
            wordsCounts.Count.Should().Be(0);
        }

        [Test]
        public void WordsCounter_ShouldReturnCountOfEveryWord()
        {
            var wordsCounter = new WordsCounter();
            var wordsCounts = wordsCounter.CountWords(new [] {"a", "b", "c", "a", "d", "b", "b"});
            wordsCounts.Count.Should().Be(4);
            wordsCounts.Should().BeEquivalentTo(new Dictionary<string, int>
            {
                {"a", 2},
                {"b", 3},
                {"c", 1},
                {"d", 1}
            });
        }
    }
}