using System.Drawing;
using NUnit.Framework;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualizationTest
{
    class RectangleExtension_Should
    {
        [Test]
        public void GetCenter_ReturnExactCenter_WhenItHasIntegerCoordinates()
        {
            Rectangle rectangle = new Rectangle(1, 1, 10, 10);
            Point expectedCenter = new Point(6, 6);

            Assert.AreEqual(expectedCenter, rectangle.GetCenter());
        }

        [Test]
        public void GetCenter_ReturnNearToCenter_WhenItDoesNotHasIntegerCoordinates()
        {
            Rectangle rectangle = new Rectangle(1, 1, 9, 9);
            PointF exactCenter = new PointF(5.5f, 5.5f);

            PointF actualCenter = rectangle.GetCenter();

            Assert.AreEqual(exactCenter.X, actualCenter.X, 1);
            Assert.AreEqual(exactCenter.Y, actualCenter.Y, 1);
        }

        [Test]
        public void IntersectsWithAny_ShouldReturnFalseWhenNoRectangles()
        {
            var rectangle = new Rectangle(0, 0, 1, 1);
            Assert.False(rectangle.IntersectsWithAny(new Rectangle[]{}));
        }

        [Test]
        public void IntersectsWithAny_ShouldReturnFalseWhenNoIntersection()
        {
            var rectangle = new Rectangle(0, 0, 1, 1);
            var otherRectangle = new Rectangle(2, 2, 1, 1);
            Assert.False(rectangle.IntersectsWithAny(new []{otherRectangle}));
        }

        [Test]
        public void IntersectsWithAny_ShouldReturnTrueWhewIntersects()
        {
            var rectangle = new Rectangle(0, 0, 3, 3);
            var otherRectangle = new Rectangle(2, 2, 1, 1);
            Assert.True(rectangle.IntersectsWithAny(new[] { otherRectangle }));
        }
    }
}
