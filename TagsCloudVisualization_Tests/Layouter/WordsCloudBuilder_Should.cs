using System;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using TagsCloudVisualization.Layouter;
using TagsCloudVisualization.WordsProcessing;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class WordsCloudBuilder_Should
    {
        private IWordsProvider wordsProvider;
        private IWeighter weighter;
        private ISizeConverter sizeConverter;
        private ICloudLayouter cloudLayouter;
        private WordsCloudBuilder wordsCloudBuilder;
        private Size defaultSize = new Size(200, 100);
        private Rectangle rectangle;
        private Font defaultFont = new Font("Times New Roman", 100);

        [SetUp]
        public void SetUp()
        {
            wordsProvider = Substitute.For<IWordsProvider>();
            weighter = Substitute.For<IWeighter>();
            sizeConverter = Substitute.For<ISizeConverter>();
            cloudLayouter = Substitute.For<ICloudLayouter>();
            wordsCloudBuilder = new WordsCloudBuilder(wordsProvider, cloudLayouter, sizeConverter, weighter);
            rectangle = new Rectangle(new Point(0, 0), defaultSize);
            cloudLayouter.PutNextRectangle(defaultSize).Returns(rectangle);
            var words = new[] {"a", "a", "b", "b", "b"};
            wordsProvider.Provide().Returns(words);
            var weighted = new[] {new WeightedWord("a", 2), new WeightedWord("b", 3)};
            weighter.WeightWords(words).Returns(weighted);
            sizeConverter.Convert(weighted).Returns(new[]{new SizedWord("a", defaultFont, defaultSize), new SizedWord("b", defaultFont, defaultSize)});
        }

        [Test]
        public void BuildCloud_And_ReturnWords()
        {
            var expected = new []{new Word("a", defaultFont, rectangle), new Word("b", defaultFont, rectangle)}; 
            wordsCloudBuilder.Build().Should().BeEquivalentTo(expected);
        }

        [Test]
        public void BuildCloud_ReturnEmptySequence_OnEmptyInput()
        {
            wordsProvider.Provide().Returns(Array.Empty<string>());
            wordsCloudBuilder.Build().Should().BeEmpty();
        }
    }
}