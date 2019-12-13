using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloud.CloudConstruction;
using TagsCloud.CloudConstruction.Extensions;
using TagsCloud.Visualization;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private CircularCloudLayouter _cloud;

        [SetUp]
        public void SetUp()
        {
            _cloud = new CircularCloudLayouter(new Point(0, 0));
        }

        private Point GetRectCenter(Rectangle rect)
        {
            return new Point(rect.Location.X + rect.Width / 2, rect.Location.Y + rect.Height / 2);
        }

        [Test]
        public void CloudsCenter_IsEqual_ToPointInConstructor()
        {
            var cloud = new CircularCloudLayouter(new Point(-5, 6));
            cloud.Center.Should().Be(new Point(-5, 6));
        }


        [Test]
        public void CloudsRectangles_IsEmpty_BeforeAnyWasPut()
        {
            _cloud.Rectangles.Should().BeEmpty();
        }

        [TestCase(-1, 1, TestName = "Negative width")]
        [TestCase(1, -1, TestName = "Negative height")]
        [TestCase(0, 1, TestName = "Zero width")]
        [TestCase(1, 0, TestName = "Zero height")]
        public void PutNextRectangle_ThrowsArgumentException_OnWrongRectangle(int width, int height)
        {
            Action act = () => _cloud.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(4, 2, TestName = "With even size")]
        [TestCase(1, 3, TestName = "With odd size")]
        public void PutNextRectangle_ReturnsCenter_OnFirstRectanglePut(int width, int height)
        {
            var center = new Point(-5, 20);
            _cloud = new CircularCloudLayouter(center);
            var rectangleSize = new Size(width, height);
            var expectedLocation = new Point(_cloud.Center.X - width / 2, _cloud.Center.Y - height / 2);

            var rectangle = _cloud.PutNextRectangle(rectangleSize);

            rectangle.Location.Should().Be(expectedLocation);
        }

        [Test]
        public void PutNextRectangle_LocateTwoRectangles_OnDifferentPoint()
        {
            var rect1 = _cloud.PutNextRectangle(new Size(1, 1));
            var rect2 = _cloud.PutNextRectangle(new Size(1, 1));

            rect1.Location.Should().NotBe(rect2.Location);
        }

        [Test]
        public void PutNextRectangle_LocateTwoRectangles_WithNoIntersections()
        {
            var rect1 = _cloud.PutNextRectangle(new Size(10, 2));
            var rect2 = _cloud.PutNextRectangle(new Size(40, 200));

            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_CollectNotIntersectingRectangles_On100Iterations()
        {
            var random = new Random();
            var count = 100;
            foreach (var size in SizeGenerator.GenerateRandomSize())
            {
                _cloud.PutNextRectangle(size);
                count--;
                if (count <= 0) break;
            }

            foreach (var rectangle in _cloud.Rectangles)
                rectangle.IntersectsWithAny(_cloud.Rectangles
                        .Where(rect => !rect.Equals(rectangle)))
                    .Should().BeFalse();
        }

        public void PutNextRectangle_PutsRectanglesInCircleShape()
        {
            var random = new Random();
            var count = 100;
            var center = _cloud.Center;
            foreach (var size in SizeGenerator.GenerateRandomSize())
            {
                _cloud.PutNextRectangle(size);
                count--;
                if (count <= 0) break;
            }

            var minX = _cloud.Rectangles.Min(rect => rect.Left);
            var minY = _cloud.Rectangles.Min(rect => rect.Top);
            var maxX = _cloud.Rectangles.Max(rect => rect.Right);
            var maxY = _cloud.Rectangles.Max(rect => rect.Bottom);
            var rad = Math.Max(maxY - minY, maxX - minX);
            // (x - center_x)^2 + (y - center_y)^2 < radius^2.

            foreach (var localRad in _cloud.Rectangles
                .Select(rect => GetRectCenter(rect))
                .Select(rectCenter => Math.Pow(rectCenter.X - center.X, 2)
                                      + Math.Pow(rectCenter.Y - center.Y, 2)))
            {
                localRad.Should().BeLessThan(Math.Pow(rad, 2));
            }
        }
    }
}