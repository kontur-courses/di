using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class PutNextRectangle_Should : TestsHandler
    {
        [Test]
        public void ReturnRectangleWithTheSameSize()
        {
            var newRectangle = layouter.PutNextRectangle(new Size(20, 15));

            newRectangle.Should().NotBeNull();
            newRectangle.Size.Should().BeEquivalentTo(newRectangle.Size);
        }

        [TestCase(-1, 15, TestName = "Negative width")]
        [TestCase(0, 15, TestName = "Zero width")]
        [TestCase(15, -1, TestName = "Negative height")]
        [TestCase(15, 0, TestName = "Zero height")]
        public void ThrowArgumentException(int width, int height)
        {
            Func<Rectangle> putting = () => layouter.PutNextRectangle(new Size(width, height));

            putting.Should().Throw<ArgumentException>();
        }

        [Test]
        public void ReturnTwoNotIntersectingRectangles()
        {
            var rnd = new Random();
            var first = layouter.PutNextRectangle(new Size(rnd.Next(1, 100), rnd.Next(1, 100)));
            var second = layouter.PutNextRectangle(new Size(rnd.Next(1, 100), rnd.Next(1, 100)));

            RectanglesChecker.HaveIntersection(first, second).Should().BeFalse();
        }

        [Test]
        public void ReturnTwoNotNestedRectangles()
        {
            var rnd = new Random();
            var first = layouter.PutNextRectangle(new Size(rnd.Next(1, 100), rnd.Next(1, 100)));
            var second = layouter.PutNextRectangle(new Size(rnd.Next(1, 100), rnd.Next(1, 100)));

            RectanglesChecker.IsNestedRectangle(first, second).Should().BeFalse();
            RectanglesChecker.IsNestedRectangle(second, first).Should().BeFalse();
        }
    }
}
