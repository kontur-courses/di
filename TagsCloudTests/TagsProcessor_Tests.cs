using System;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloud;
using TagsCloud.Interfaces;

namespace TagsCloudTests
{
	[TestFixture]
	public class TagsProcessor_Tests
	{
		private TagsProcessor tagsProcessor;
		private IWordsProcessor wordsProcessor;
		private FontSettings settings;
		private IImageHolder imageHolder;

		[SetUp]
		public void SetUp()
		{
			wordsProcessor = Substitute.For<IWordsProcessor>();
			settings = new FontSettings();
			imageHolder = Substitute.For<IImageHolder>();
			tagsProcessor = new TagsProcessor(wordsProcessor, settings, imageHolder);
		}

		[TestCase(1, 5, 15, TestName = "Min_Size_Grater_Than_Max_Size")]
		[TestCase(0, 5, 15, TestName = "Word_Frequency_Less_Than_One")]
		[TestCase(1, -1, 15, TestName = "Max_Size_Less_Than_One")]
		[TestCase(1, 1, -15, TestName = "Min_Size_Less_Than_One")]
		public void CalculateFontSize_ThrowsException_When(int wordFrequency, int maxSize, int minSize)
		{
			settings.MaxFontSize = maxSize;
			settings.MinFontSize = minSize;
			var word = new Word("a", wordFrequency);
			
			Action action = () => tagsProcessor.CalculateFontSize(word);
			action.Should().Throw<ArgumentException>();
		}

		[Test]
		public void CalculateFontSize_ReturnsSameSizes_WhenMaxAndMinSizesAreEqual()
		{
			const int expectedSize = 15;
			settings.MaxFontSize = expectedSize;
			settings.MinFontSize = expectedSize;
			var randomizer = new Random();
			var words = Enumerable.Range(0, 10)
				.Select(_ => randomizer.Next(5, 30))
				.Select(s => new Word("a", s));

			var actualSizes = words.Select(w => tagsProcessor.CalculateFontSize(w));
			actualSizes.Should().AllBeEquivalentTo(expectedSize);
		}

		[TestCase(100, 10, 50, 50, TestName = "Max_Size_When_Calculated_Size_Grater_Than_Max_Size")]
		[TestCase(2, 10, 50, 10, TestName = "Min_Size_When_Calculated_Size_Less_Than_Min_Size")]
		[TestCase(1, 10, 50, 10, TestName = "Min_Size_When_Calculated_Size_Is_Zero")]
		[TestCase(50, 10, 50, 45, TestName = "45_When_Word_Frequency_Is_50")]
		public void CalculateFontSize_Returns(int wordFrequency,
												int minFontSize, 
												int maxFontSize,
												int expectedFontSize)
		{
			settings.MaxFontSize = maxFontSize;
			settings.MinFontSize = minFontSize;
			var word = new Word("a", wordFrequency);

			var actualFontSize = tagsProcessor.CalculateFontSize(word);
			actualFontSize.Should().Be(expectedFontSize);
		}
	}
}