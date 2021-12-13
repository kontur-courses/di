using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Visualization;

namespace CloudTagVisualizer.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        [TestCase(0, 0, TestName = "Center point is (0,0)")]
        [TestCase(10, 20, TestName = "Center point is not (0,0)")]
        public void ReturnRectangleWithCenterInStartPosition_OnFirstIteration_When(int x, int y)
        {
            var center = new Point(x, y);
            var size = new Size(500, 300);
            var expectedRectangle = new Rectangle(center - size / 2, size);
            var layouter = new CircularCloudLayouter(new ExpandingSquare())
            {
                Center = center
            };

            var actualRectangle = layouter.PutNextRectangle(size);

            actualRectangle.Should().BeEquivalentTo(expectedRectangle);
        }

        [Test]
        public void ReturnRectanglesAroundCenter_WhenSquaresWithSameSizeGiven()
        {
            var center = new Point(5, 6);
            var layouter = new CircularCloudLayouter(new ExpandingSquare())
            {
                Center = center
            };

            var size = new Size(2, 2);
            var startPoint = center - size / 2;
            var horizontalOffset = new Size(size.Width, 0);
            var verticalOffset = new Size(0, size.Height);
            var expected = new List<Rectangle>()
            {
                new Rectangle(startPoint, size),
                new Rectangle(startPoint - verticalOffset - horizontalOffset, size),
                new Rectangle(startPoint - verticalOffset, size),
                new Rectangle(startPoint - verticalOffset + horizontalOffset, size),
                new Rectangle(startPoint + horizontalOffset, size),
                new Rectangle(startPoint + horizontalOffset + verticalOffset, size),
                new Rectangle(startPoint + verticalOffset, size),
                new Rectangle(startPoint + verticalOffset - horizontalOffset, size),
                new Rectangle(startPoint - horizontalOffset, size),
            };

            var actual = Enumerable
                .Range(1, expected.Count)
                .Select(_ => layouter.PutNextRectangle(size))
                .ToList();
            actual.Should().BeEquivalentTo(expected,
                config =>
                    config.WithStrictOrdering());
        }

        [Test]
        public void ReturnNotIntersectedRectangles()
        {
            var rectanglesCount = 50;
            var center = new Point(100, 200);
            var sizes = new Size[rectanglesCount];
            var random = new Random(12345);
            for (var i = 0; i < rectanglesCount; i++)
            {
                sizes[i] = new Size(random.Next(1, 100), random.Next(1, 100));
            }

            var layouter = new CircularCloudLayouter(new ExpandingSquare())
            {
                Center = center
            };

            var rectangles = sizes.Select(size => layouter.PutNextRectangle(size)).ToArray();

            for (int i = 0; i < rectanglesCount; i++)
            {
                for (int j = i + 1; j < rectanglesCount; j++)
                {
                    rectangles[i].IntersectsWith(rectangles[j]).Should().BeFalse();
                }
            }
        }

        [Test]
        public void ReturnRectanglesWithTightDistribution()
        {
            var center = new Point(100, 200);
            var sizes = new Size[]
            {
                new Size(100, 100),
                new Size(50, 100),
                new Size(100, 50),
                new Size(100, 200),
                new Size(200, 100),
                new Size(150, 150),
                new Size(300, 300),
                new Size(400, 400)
            };

            var layouter = new CircularCloudLayouter(new ExpandingSquare())
            {
                Center = center
            };
            var rectangles = sizes.Select(x => layouter.PutNextRectangle(x)).ToArray();

            var maxDistance = 0d;
            var allowedMaxDistance = 1000;
            foreach (var rect1 in rectangles)
            {
                foreach (var rect2 in rectangles)
                {
                    var deltaLocation = rect1.Location - (Size) rect2.Location;
                    maxDistance = Math.Max(maxDistance,
                        Math.Sqrt(deltaLocation.X * deltaLocation.X) + Math.Sqrt(deltaLocation.Y * deltaLocation.Y));
                }
            }

            maxDistance.Should().BeLessThan(allowedMaxDistance);
        }
    }
}