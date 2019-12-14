using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagCloud.Infrastructure;
using TagCloud.WordsProcessing;

namespace TagCloudTests.WordsProcessing
{
    public class WordProcessorTests
    {
        private IWordCounter wordCounter;
        private IWordSelector wordSelector;

        [SetUp]
        public void SetUp()
        {
            wordCounter = Substitute.For<IWordCounter>();
            wordCounter.GetCountedWords(Arg.Any<IEnumerable<Word>>()).Returns(w => w[0]);
            wordSelector = Substitute.For<IWordSelector>();
            wordSelector.IsSelectedWord(null).ReturnsForAnyArgs(true);
        }


        [Test]
        public void PrepareWords_ShouldReturnLoweredWords_OnWordsInCaps()
        {
            var words = new List<string> { "WORD1", "word2", "WoRd3" };
            var expectedWordsValues = new List<string> { "word1", "word2", "word3" };
            var processor = new WordProcessor(wordSelector, wordCounter);

            var result = processor.PrepareWords(words);

            result.Select(w => w.Value).Should().BeEquivalentTo(expectedWordsValues);
        }

        [Test]
        public void PrepareWords_ShouldReturnOneWord_OnSameWordUpperAndLower()
        {
            var words = new List<string> { "word", "WORD", "wOrD" };
            var expectedWordsValue = new Word("word");
            var processor = new WordProcessor(wordSelector, wordCounter);

            var result = processor.PrepareWords(words);

            result.Should().AllBeEquivalentTo(expectedWordsValue);
        }

        [Test]
        public void PrepareWords_ShouldReturnAllWords_OnAllSelected()
        {
            wordSelector.IsSelectedWord(Arg.Any<Word>()).ReturnsForAnyArgs(x => ((Word)x[0]).Value == "word");
            var words = new List<string> { "word", "word", "word" };
            var processor = new WordProcessor(wordSelector, wordCounter);

            var result = processor.PrepareWords(words);

            result.Select(w => w.Value).Should().BeEquivalentTo(words);
        }

        [Test]
        public void PrepareWords_ShouldReturnEmptyCollection_OnNoneSelected()
        {
            wordSelector.IsSelectedWord(Arg.Any<Word>()).ReturnsForAnyArgs(x => ((Word)x[0]).Value == "word");
            var words = new List<string> { "word1", "word2", "word3" };
            var processor = new WordProcessor(wordSelector, wordCounter);

            var result = processor.PrepareWords(words);

            result.Should().BeEmpty();
        }

        [Test]
        public void PrepareWords_ShouldReturnOnlySelectedWords_OnSomeSelected()
        {
            wordSelector.IsSelectedWord(Arg.Any<Word>()).ReturnsForAnyArgs(x => ((Word)x[0]).Value == "word");
            var words = new List<string> { "word1", "word2", "word" };
            var expectedWordsValue = new Word("word");
            var processor = new WordProcessor(wordSelector, wordCounter);

            var result = processor.PrepareWords(words);

            result.Should().HaveCount(1).And.AllBeEquivalentTo(expectedWordsValue);
        }
    }
}
