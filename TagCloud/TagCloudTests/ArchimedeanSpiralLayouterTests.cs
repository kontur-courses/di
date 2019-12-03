using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using System.Drawing;
using TagCloud;

namespace TagsCloudTests
{
    [TestFixture]
    public class ArchimedeanSpiralLayouterTests
    {
        public class PutNextRectangleShould
        {
            private ArchimedeanSpiralLayouter defaultLayouter;

            private SizeF defaultSize = new SizeF(5, 2);

            private PointF defaultCenter;

            private ArchimedeanSpiralLayouter CreateBaseLayouter()
            {
                return new ArchimedeanSpiralLayouter(LayouterSettings.GetDefaultSettings(), ImageSettings.GetDefaultSettings());
            }

            [SetUp]
            public void BaseSetUp()
            {
                defaultCenter = ImageSettings.GetDefaultSettings().CloudCenter;
                defaultLayouter = CreateBaseLayouter();
            }

            private bool AreIntersecting(List<RectangleF> rectangles)
            {
                var areIntersecting = false;
                for (int i = 0; i < rectangles.Count; ++i)
                    for (int j = 0; j < rectangles.Count; ++j)
                    {
                        var rectangleX = rectangles[i];
                        var rectangleY = rectangles[j];
                        if (i != j && rectangleX.IntersectsWith(rectangleY))
                            areIntersecting = true;
                    }
                return areIntersecting;
            }

            [TestCase(-1, 1)]
            [TestCase(1, -1)]
            [TestCase(0, 1)]
            public void ThrowArgumentException_OnNegativeOrZeroRectangleSize(int height, int width)
            {
                var size = new SizeF(height, width);
                Action getRectangle = () => defaultLayouter.PutNextRectangle(size);
                getRectangle.Should().Throw<ArgumentException>().WithMessage("Invalid size");
            }

            [Test]
            public void PutFirstRectangleInCenter()
            {
                var rectangle = defaultLayouter.PutNextRectangle(defaultSize);
                rectangle.X.Should().Be(defaultCenter.X - defaultSize.Width / 2);
                rectangle.Y.Should().Be(defaultCenter.Y - defaultSize.Height / 2);
            }

            [TestCase(2, 2)]
            [TestCase(1, 10)]
            [TestCase(6, 3)]
            public void PlaceSameRectanglesWithoutIntersection(int height, int width)
            {
                var size = new SizeF(height, width);
                var rectangles = new List<RectangleF>();
                for (int i = 0; i < 10; ++i)
                {
                    rectangles.Add(defaultLayouter.PutNextRectangle(size));
                }
                AreIntersecting(rectangles).Should().BeFalse();
            }

            [Test]
            public void PlaceDifferentRectanglesWithoutIntersection()
            {
                var rectangles = new List<RectangleF>();
                var size = new SizeF(2, 1);
                for (int i = 0; i < 10; ++i)
                {
                    rectangles.Add(defaultLayouter.PutNextRectangle(size));
                    size.Height++;
                    size.Width++;
                }
                AreIntersecting(rectangles).Should().BeFalse();
            }

            [Test]
            public void NotPlaceRectangles_OnNegativeCoordinates()
            {
                var rectangles = new List<RectangleF>();
                var size = new SizeF(1000, 1000);
                for (int i = 0; i < 10; ++i)
                {
                    rectangles.Add(defaultLayouter.PutNextRectangle(size));
                }
                rectangles.Any(rectangle => rectangle.X < 0 || rectangle.Y < 0).Should().BeFalse();
            }

            [Test, Timeout(1000)]
            public void PlaceRectanglesFast()
            {
                var size = new SizeF(1, 1);
                for (int i = 0; i < 10000; ++i)
                    defaultLayouter.PutNextRectangle(size);
            }

            private double GetDistanceToCenter(RectangleF rectangle) =>
                Math.Sqrt(Math.Pow(rectangle.X - defaultCenter.X, 2) + Math.Pow(rectangle.Y - defaultCenter.Y, 2));

            [TestCase(5, 2)]
            [TestCase(10, 20)]
            [TestCase(10, 10)]
            [TestCase(100, 100)]
            [TestCase(1, 1)]
            public void PlaceRectanglesTightly(int width, int height)
            {
                var size = new Size(width, height);
                var rectangles = new List<RectangleF>();
                for (int i = 0; i < 500; ++i)
                    rectangles.Add(defaultLayouter.PutNextRectangle(size));
                var maxRadius = rectangles
                    .Max(rectangle => GetDistanceToCenter(rectangle));
                var cloudMaxArea = maxRadius * maxRadius * Math.PI;
                var squareArea = rectangles
                    .Select(rectangle => rectangle.Height * rectangle.Width)
                    .Sum();
                var ratio = cloudMaxArea / squareArea;
                ratio.Should().BeLessThan(5);
            }
        }
    }
}