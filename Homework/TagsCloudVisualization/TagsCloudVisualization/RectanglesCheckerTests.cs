using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class RectangleChecker_Should : TestsHandler
    {
        [TestCase(10, 15, 10, 2, TestName = "Left side")]
        [TestCase(10, 15, 5, 2, TestName = "Left side touching")]
        [TestCase(25, 15, 10, 2, TestName = "Right side")]
        [TestCase(30, 15, 10, 2, TestName = "Right side touching")]
        [TestCase(20, 5, 2, 10, TestName = "Top side")]
        [TestCase(20, 5, 2, 5, TestName = "Top side touching")]
        [TestCase(20, 20, 2, 10, TestName = "Bottom side")]
        [TestCase(15, 25, 2, 10, TestName = "Bottom side touching")]
        [TestCase(10, 15, 30, 2, TestName = "Left and right sides")]
        [TestCase(20, 15, 2, 30, TestName = "Top and bottom sides")]
        [TestCase(15, 10, 15, 15, TestName = "The same")]
        public void HaveIntersection_ReturnTrue(int x, int y, int width, int height)
        {
            var staticRectangle = layouter.PutNextRectangle(new Size(15, 15), new Point(15, 10));
            var rectangle = layouter.PutNextRectangle(new Size(width, height), new Point(x, y));

            RectanglesChecker.HaveIntersection(staticRectangle, rectangle).Should().BeTrue();
        }

        [TestCase(50, 50, 2, 2, TestName = "Not intersecting")]
        [TestCase(10, 5, 2, 2, TestName = "One in another")]
        public void HaveIntersection_ReturnFalse(int x, int y, int width, int height)
        {
            var staticRectangle = layouter.PutNextRectangle(new Size(15, 15), new Point(5, 0));
            var rectangle = layouter.PutNextRectangle(new Size(width, height), new Point(x, y));

            RectanglesChecker.HaveIntersection(rectangle, staticRectangle).Should().BeFalse();
        }

        [TestCase(10, 10, 2, 2, TestName = "One in another")]
        public void IsNestedRectangle_ReturnTrue_OneInAnother(int x, int y, int width, int height)
        {
            var nestedRectangle = layouter.PutNextRectangle(new Size(width, height), new Point(x, y));
            var mainRectangle = layouter.PutNextRectangle(new Size(20, 20), new Point(5, 5));

            RectanglesChecker.IsNestedRectangle(nestedRectangle, mainRectangle).Should().BeTrue();
        }

        [TestCase(5, 5, 10, 10, TestName = "Another in one")]
        [TestCase(50, 50, 2, 2, TestName = "Not one in another")]
        [TestCase(5, 5, 20, 20, TestName = "The same")]
        [TestCase(5, 5, 5, 5, TestName = "With touching")]
        public void IsNestedRectangle_ReturnFalse(int x, int y, int width, int height)
        {
            var nestedRectangle = layouter.PutNextRectangle(new Size(width, height), new Point(x, y));
            var mainRectangle = layouter.PutNextRectangle(new Size(20, 20), new Point(5, 5));

            RectanglesChecker.IsNestedRectangle(nestedRectangle, mainRectangle).Should().BeFalse();
        }
    }
}
