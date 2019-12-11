using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.Algorithm;
using TagsCloudContainer.Algorithm.SizeProviding;
using TagsCloudContainer.Extensions;

namespace TagsCloudContainerTests
{
    public class CorrespondingToWeightSizeProviderTests
    {
        private CorrespondingToWeightSizeProvider sizeProvider;
        private const int Error = 4;

        [SetUp]
        public void SetUp()
        {
            sizeProvider = new CorrespondingToWeightSizeProvider(Error);
        }


        [Test]
        public void SetWordsSizes_ShouldReturnSizeWhereWidthIsTwoTimesMoreThanHeight()
        {
            var words = new[] {new Word()};
            var pictureSize = new Size(100, 100);

            var result = sizeProvider.SetWordsSizes(words, pictureSize);
            var resultSize = result.First().Size;

            resultSize.Width.Should().Be(resultSize.Height * 2);
        }

        [Test]
        public void SetWordsSizes_ShouldReturnSizeProportionalToImageSize_WhenOnlyOneWordInInput()
        {
            var words = new[] {new Word()};
            var pictureSize = new Size(80, 40);

            var result = sizeProvider.SetWordsSizes(words, pictureSize);
            var resultSize = result.First().Size;

            resultSize.GetArea().Should()
                .BeInRange(pictureSize.GetArea() / Error - 40, pictureSize.GetArea() / Error + 40);
        }

        [Test]
        public void SetWordsSizes_ShouldReturnSizesProportionalToWeights()
        {
            var words = new[] {new Word {Weight = 2}, new Word {Weight = 1},};
            var pictureSize = new Size(80, 40);

            var result = sizeProvider.SetWordsSizes(words, pictureSize).ToList();

            (result[0].Size.GetArea() / result[1].Size.GetArea()).Should()
                .BeInRange(result[0].Weight / result[1].Weight - 40, result[0].Weight / result[1].Weight + 40);
        }

        [Test]
        public void SetWordsSizes_ShouldReturnFirstSizeProportionalToWordsCount_WhenManyWordsInInput()
        {
            var words = new[] {new Word {Weight = 4}, new Word {Weight = 2}, new Word {Weight = 1},};
            var pictureSize = new Size(80, 40);

            var result = sizeProvider.SetWordsSizes(words, pictureSize);
            var firstSize = result.First().Size;

            firstSize.GetArea().Should().BeInRange(pictureSize.GetArea() / (words.Length * Error) - 40,
                pictureSize.GetArea() / (words.Length * Error) + 40);
        }
    }
}