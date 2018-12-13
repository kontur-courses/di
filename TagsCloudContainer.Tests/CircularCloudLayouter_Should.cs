using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.CloudLayouter;

namespace TagsCloudContainer.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private int imageWidth;
        private int imageHeight;
        private ICloudLayouterSettings settings;
        private ICloudLayouter cloudLayouter;
        private Size defaultRectangle;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            imageWidth = 2048;
            imageHeight = 1024;

            settings = A.Fake<ICloudLayouterSettings>();
            A.CallTo(() => settings.ImageWidth)
                .Returns(imageWidth);
            A.CallTo(() => settings.ImageHeight)
                .Returns(imageHeight);
            A.CallTo(() => settings.CenterX)
                .Returns(0);
            A.CallTo(() => settings.CenterY)
                .Returns(0);

            defaultRectangle = new Size(200, 50);
        }

        [SetUp]
        public void SetUp()
        {
            cloudLayouter = new CircularCloudLayouter(settings);
        }

        [TestCase(0, 0, TestName = "in the center of the coordinate system")]
        [TestCase(50, 0, TestName = "on axis OX")]
        [TestCase(-50, -50, TestName = "on third part of the coordinate system")]
        public void PutNextRectangleF_FirstRectangle_PutToCenter(int centerX, int centerY)
        {
            A.CallTo(() => settings.CenterX)
                .Returns(centerX);
            A.CallTo(() => settings.CenterY)
                .Returns(centerY);
            var expected = new RectangleF(centerX + imageWidth / 2f, centerY + imageHeight / 2f,
                defaultRectangle.Width, defaultRectangle.Height);

            cloudLayouter.PutNextRectangleF(defaultRectangle)
                .Should()
                .BeEquivalentTo(expected);
        }

        [TestCase(0, 0, TestName = "with zero width and height")]
        [TestCase(0, 50, TestName = "with zero width")]
        [TestCase(50, 0, TestName = "with zero height")]
        [TestCase(-50, -50, TestName = "with negative width and height")]
        [TestCase(-50, 50, TestName = "with negative width")]
        [TestCase(50, -50, TestName = "with negative height")]
        public void PutNextRectangle_InvalidRectangleSize_ThrowException(int width, int height)
        {
            var invalidRectangle = new Size(width, height);
            Action act = () => cloudLayouter.PutNextRectangleF(invalidRectangle);

            act.Should()
                .Throw<ArgumentException>();
        }

        [TestCase(100, 100, TestName = "square")]
        [TestCase(500, 200, TestName = "rectangle")]
        public void PutNextRectangle_ValidRectangleSize_PutSuccessfully(int width, int height)
        {
            var validRectangle = new Size(width, height);

            cloudLayouter.PutNextRectangleF(validRectangle)
                .Should()
                .BeOfType<RectangleF>();
        }

        [TestCase(1, TestName = "one rectangle")]
        [TestCase(2, TestName = "two rectangles")]
        [TestCase(10, TestName = "ten rectangles")]
        [TestCase(50, TestName = "fifty rectangles")]
        [TestCase(100, TestName = "one hundred rectangles")]
        [TestCase(1000, TestName = "one thousand rectangles")]
        public void PutNextRectangle_SeveralRectangles_NotIntersect(int count)
        {
            var rectangles = GetRandomRectangles(count);

            var placedRectangles = rectangles.Select(nextRectangle => cloudLayouter.PutNextRectangleF(nextRectangle))
                .ToArray();

            placedRectangles.Any(rect => IsIntersectWithAnyAnotherRectangle(rect, placedRectangles))
                .Should()
                .BeFalse();
        }

        private static bool IsIntersectWithAnyAnotherRectangle(RectangleF rectangle, IEnumerable<RectangleF> rectangles)
        {
            return rectangles.Where(rect => !rectangle.Equals(rect))
                .Any(r => r.IntersectsWith(rectangle));
        }

        private static IEnumerable<SizeF> GetRandomRectangles(int count)
        {
            var random = new Random();

            return Enumerable.Range(0, count)
                .Select(x => new SizeF(random.Next(100, 200), random.Next(20, 100)));
        }
    }
}