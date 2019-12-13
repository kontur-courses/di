using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Drawing;
using FluentAssertions;
using TagsCloudApp.LayOuter;

namespace TagsCloudApp.Test
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter layouter;
        private List<Rectangle> rectangles;
        private readonly Point center = new Point(100, 50);

        [SetUp]
        public void CreateCircularCloudLayouter()
        {
            layouter = new CircularCloudLayouter(center);
            rectangles = new List<Rectangle>();
        }

        [Test]
        public void Constructor_ThrowArgumentException_OnNegativeCoordinate()
        {
            Assert.Throws<ArgumentException>(() => new CircularCloudLayouter(new Point(-50, 50)));
        }


        [Test]
        public void PutNextRectangle_ShouldPlace_FirstRectangleToCenter()
        {
            rectangles.Add(layouter.PutNextRectangle(new Size(20, 10)));
            var location = rectangles[0].Location;
            var expectedLocation = new Point(90, 45);
            location.Should().Be(expectedLocation);
        }


        [Test]
        public void PutNextRectangle_ShouldPlaceTwoRectangles_WithoutInsersection()
        {
            rectangles.Add(layouter.PutNextRectangle(new Size(20, 10)));
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 5)));
            rectangles[0].IntersectsWith(rectangles[1]).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_ShouldPlaceTwoRectangles_Near()
        {
            rectangles.Add(layouter.PutNextRectangle(new Size(20, 10)));
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 5)));
            rectangles[0].Bottom.Should().Be(rectangles[1].Y);
        }

        [Test]
        public void PutNextRectangle_ShouldPlaceFourRectangles_NearCenterRectangle()
        {
            rectangles.Add(layouter.PutNextRectangle(new Size(20, 10)));
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 5)));
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 5)));
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 5)));
            rectangles.Add(layouter.PutNextRectangle(new Size(10, 5)));
            rectangles[1].Y.Should().BeLessOrEqualTo(rectangles[0].Bottom + 6);
            rectangles[2].Y.Should().BeLessOrEqualTo(rectangles[0].Bottom + 6);
            rectangles[3].Y.Should().BeLessOrEqualTo(rectangles[0].Bottom + 6);
            rectangles[4].Y.Should().BeLessOrEqualTo(rectangles[0].Bottom + 6);
        }

        private List<Size> GetRandomSizes()
        {
            var sizes = new List<Size>();
            Random rnd = new Random();
            for (var i = 0; i < 30; i++)
            {
                var value1 = rnd.Next(5, 70);
                var value2 = rnd.Next(5, 70);
                sizes.Add(new Size(value1, value2));
            }
            return sizes;
        }


        [Test]
        public void PutNextRectangle_RectanglesShouldNotIntersect()
        {
            var sizes = GetRandomSizes();
            foreach (var size in sizes)
            {
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            for (var i = 0; i < rectangles.Count; i++)
            {
                for (var j = 0; j < rectangles.Count; j++)
                {
                    if (i == j)
                        continue;
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
                }
            }
        }
    }
}
