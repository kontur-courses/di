using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagCloudGenerator.Extensions;
using TagCloudGenerator.Tests.Extensions;

namespace TagCloudGenerator.Tests.TestFixtures
{
    public class ExtensionsTests
    {
        private const double Precision = 0.7072; // sqrt(2)/2.

        [Test]
        public void SequenceShuffle_OnInputSequence_ReturnsSequenceWithSameItems()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var sequence = randomizer.GetRandomSequence().Take(100).ToArray();

            sequence.SequenceShuffle(randomizer).Should().BeEquivalentTo(sequence);
        }

        [Test]
        public void SequenceShuffle_OnInputSequence_ReturnsSequenceWithDifferentOrder()
        {
            var randomizer = TestContext.CurrentContext.Random;
            var sequence = randomizer.GetRandomSequence().Take(500).ToArray();

            sequence.SequenceShuffle(randomizer).Should().NotEqual(sequence);
        }

        [Test]
        public void CreateMovedCopy_ReturnsNewRectangle()
        {
            var rectangle = Rectangle.Empty;

            rectangle.CreateMovedCopy(Size.Empty).Should().NotBeSameAs(rectangle);
        }

        [Test]
        public void CreateMovedCopy_ReturnsRectangleMovedOnSpecifiedOffset()
        {
            var rectangle = Rectangle.Empty;
            var offset = new Size(10, 10);

            rectangle.CreateMovedCopy(offset).X.Should().Be(rectangle.X + offset.Width);
            rectangle.CreateMovedCopy(offset).Y.Should().Be(rectangle.Y + offset.Height);
        }

        [Test]
        [Category("SupportTests")]
        public void GetRectangleCenter_RandomRectangle(
            [Random(0, 1000, 1)] int xLocation, [Random(0, 1000, 1)] int yLocation,
            [Random(1, 1000, 1)] int width, [Random(1, 1000, 1)] int height)
        {
            var location = new Point(xLocation, yLocation);
            var rectangle = new Rectangle(location, new Size(width, height));
            var center = rectangle.GetRectangleCenter();

            rectangle.CheckIfPointIsCenterOfRectangle(center, Precision).Should().BeTrue();
        }

        [Category("SupportTests")]
        [TestCase(0, 0, TestName = "WithOriginAsCenter")]
        [TestCase(2, -3, TestName = "WithOddCenterCoordinates")]
        public void GetRectangleWithCenterInThePoint(int xCenter, int yCenter)
        {
            var center = new Point(xCenter, yCenter);
            var size = new Size(2, 3);
            var centeredRectangle = center.GetRectangleWithCenterInThePoint(size);

            centeredRectangle.CheckIfPointIsCenterOfRectangle(center, Precision).Should().BeTrue();
        }
    }
}