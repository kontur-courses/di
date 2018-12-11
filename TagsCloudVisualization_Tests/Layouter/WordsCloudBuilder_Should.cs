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
            weighter = Substitute.For<IWeighter>();
            sizeConverter = Substitute.For<ISizeConverter>();
            cloudLayouter = Substitute.For<ICloudLayouter>();
            wordsCloudBuilder = new WordsCloudBuilder(cloudLayouter, sizeConverter, weighter);
            rectangle = new Rectangle(new Point(0, 0), defaultSize);
            cloudLayouter.PutNextRectangle(defaultSize).Returns(rectangle);
            weighter.WeightWords().Returns(new[] {new WeightedWord("a", 6), new WeightedWord("b", 3)});
            sizeConverter.Convert(weighter.WeightWords()).Returns(new[]{new SizedWord("a", defaultFont, defaultSize), new SizedWord("b", defaultFont, defaultSize)});
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
            sizeConverter.Convert(weighter.WeightWords()).Returns(Enumerable.Empty<SizedWord>());
            wordsCloudBuilder.Build().Should().BeEmpty();
        }
    }
}