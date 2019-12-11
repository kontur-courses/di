using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagsCloudTests
{
	[TestFixture]
	public class WordsProcessor_Tests
	{
		private WordsProcessor wordsProcessor;
		private ITextReader textReader;
		private List<IWordFilter> wordFilters;

		[SetUp]
		public void SetUp()
		{
			textReader = Substitute.For<ITextReader>();
			wordFilters = new List<IWordFilter>();
			wordsProcessor = new WordsProcessor(textReader, wordFilters);
		}

		[Test]
		public void GetWordsWithFrequencies_DoesNotReturnsRepeatingWords()
		{
			textReader.Read().Returns(new[] {"a", "a", "a", "a"});

			var actualCollection = wordsProcessor.GetWordsWithFrequencies();
			actualCollection.Should().HaveCount(1);
		}
		
		[Test]
		public void GetWordsWithFrequencies_OrdersDescendingWordsByItsFrequency()
		{
			textReader.Read().Returns(new[] {"a", "a", "b", "b", "b", "c"});
			
			var actualCollection = wordsProcessor.GetWordsWithFrequencies();
			actualCollection.Should().BeInDescendingOrder(w => w.Frequency);
		}

		[Test]
		public void GetWordsWithFrequencies_ReturnsEmptyCollection_WhenReadEmptyFile()
		{
			textReader.Read().Returns(new string[0]);
			var expectedCollection = Enumerable.Empty<Word>();

			var actualCollection = wordsProcessor.GetWordsWithFrequencies();
			actualCollection.Should().BeEquivalentTo(expectedCollection);
		}

		[Test]
		public void GetWordsWithFrequencies_FilterWordsWithAllAddedFilters()
		{
			textReader.Read().Returns(new[] {"a", "here", "home"});
			wordFilters.Add(new WordLengthFilter());
			var someFilter = Substitute.For<IWordFilter>();
			someFilter.CheckWord(Arg.Any<string>()).Returns(call => (string) call[0] != "here");
			wordFilters.Add(someFilter);
			var expectedWords = new[] {"home"};

			var actualWords = wordsProcessor.GetWordsWithFrequencies().Select(w => w.Text);
			actualWords.Should().BeEquivalentTo(expectedWords);
		}
		
		[Test]
		public void GetWordsWithFrequencies_ReturnsWordsInLowerCase()
		{
			textReader.Read().Returns(new[] {"HELLO", "MINE"});
			var expectedWords = new[] {"hello", "mine"};

			var actualWords = wordsProcessor.GetWordsWithFrequencies().Select(w => w.Text);
			actualWords.Should().BeEquivalentTo(expectedWords);
		}

		[Test]
		public void GetWordsWithFrequencies_CountsFrequenciesCorrectly()
		{
			var sourceWords = new[] {"a", "b", "c", "B", "C", "A"};
			textReader.Read().Returns(sourceWords);

			var actualFrequencies = wordsProcessor.GetWordsWithFrequencies().Select(w => w.Frequency);
			actualFrequencies.Should().AllBeEquivalentTo(2);
		}
	}
}