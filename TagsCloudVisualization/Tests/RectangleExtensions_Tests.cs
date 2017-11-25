using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;

namespace TagsCloudVisualization
{
    public class RectangleExtensions_Tests
    {
        [TestFixture]
        public class RectangleExteinsions_Should
        {


            [Test]
            public void IntersectWithAny_ShouldBeTrue_OnIntersectingWithAllRectangles()
            {
                var rectangles = new List<Rectangle>
                {
                    new Rectangle(5, 5, 10, 10),
                    new Rectangle(15, 15, 10, 10)
                };
                var myRectangle = new Rectangle(12, 12, 10, 10);
                myRectangle.IntersectWithAny(rectangles).Should().BeTrue();
            }

            [Test]
            public void IntersectWithAny_ShouldBeTrue_OnIntersectingWithOneRectangle()
            {
                var rectangles = new List<Rectangle>
                {
                    new Rectangle(5, 5, 10, 10),
                    new Rectangle(15, 15, 10, 10)
                };
                var myRectangle = new Rectangle(15, 23, 10, 10);
                myRectangle.IntersectWithAny(rectangles).Should().BeTrue();
            }

            [Test]
            public void IntersectWithAny_ShouldBeFalse_WhenDontIntersect()
            {
                var rectangles = new List<Rectangle>
                {
                    new Rectangle(5, 5, 10, 10),
                    new Rectangle(15, 15, 10, 10)
                };
                var myRectangle = new Rectangle(0, 0, 2, 2);
                myRectangle.IntersectWithAny(rectangles).Should().BeFalse();
            }


            [Test]
            public void DistanceTo_Test()
            {
                var myRectangle = new Rectangle(0, 0, 10, 10);
                var center = new Vector(9, 8);
                myRectangle.DistanceTo(center).Should().Be(5);
            }

            [Test]
            public void TopLeft_Test()
            {
                var rectangle = new Rectangle(10, 10, 10, 10);
                var expectedTopLeft = new Vector(10, 10);
                rectangle.TopLeft().Should().Be(expectedTopLeft);
            }

            [Test]
            public void Center_Test()
            {
                var rectangle = new Rectangle(0, 0, 10, 10);
                var expectedCenter = new Vector(5, 5);
                rectangle.Center().Should().Be(expectedCenter);
            }
        }
    }
}