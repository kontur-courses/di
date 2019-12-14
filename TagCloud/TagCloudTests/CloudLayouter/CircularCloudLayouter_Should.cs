using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using GroboContainer.Core;
using NUnit.Framework;
using TagCloud.CloudLayouter;
using TagCloud.CloudVisualizerSpace.CloudViewConfigurationSpace;

namespace TagCloudTests.CloudLayouter
{
    [TestFixture]
    class CircularCloudLayouterShould
    {
        private CircularCloudLayouter cloudLayouter;
        private CloudViewConfiguration cloudConfiguration;
        private Container container;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            container = TagCloud.Program.InitializeContainer();
            cloudConfiguration = container.Get<CloudViewConfiguration>();
        }

        [SetUp]
        public void SetUp()
        {
            cloudLayouter = container.Create<CircularCloudLayouter>();
        }

        [TestCase(-2, 5, TestName = "Negative width")]
        [TestCase(2, -5, TestName = "Negative height")]
        [TestCase(0, 5, TestName = "Zero width")]
        [TestCase(2, 0, TestName = "Zero height")]
        public void PutNextRectangle_ThrowArgumentException_WhenIncorrectDirectionsOfRectangle(int width, int height)
        {
            Action action = () => cloudLayouter.PutNextRectangle(new Size(width, height));

            action.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, TestName = "Zero center")]
        [TestCase(10, 20, TestName = "Non-zero center")]
        public void AddingOneRectangle_PlaceInCenter(int cloudCenterX, int cloudCenterY)
        {
            var cloudCenter = new Point(cloudCenterX, cloudCenterY);
            var rectangleSize = new Size(20, 10);
            var expectedRectangle = new Rectangle(cloudCenter, rectangleSize);

            var tempCenter = cloudConfiguration.CloudCenter;
            cloudConfiguration.CloudCenter = cloudCenter;

            var actualRectangle = cloudLayouter.PutNextRectangle(rectangleSize);

            actualRectangle.Should().Be(expectedRectangle);

            cloudConfiguration.CloudCenter = tempCenter;
        }

        [TestCase(2, 2, 5, false, TestName = "Small rectangles no snuggle")]
        [TestCase(100, 100, 10, false, TestName = "CommonRectangles no snuggle")]
        [TestCase(100, 100, 100, false, TestName = "Many Rectangles no snuggle")]
        [TestCase(2, 2, 5, true, TestName = "Small rectangles with snuggle")]
        [TestCase(100, 100, 10, true, TestName = "CommonRectangles with snuggle")]
        [TestCase(100, 100, 100, true, TestName = "Many Rectangles with snuggle")]
        public void PutNextRectangle_HaveNoIntersections(int maxWidth, int maxHeight, int rectanglesCount, bool needSnuggle)
        {
            cloudConfiguration.NeedSnuggle = needSnuggle;

            var rectangles = new List<Rectangle>();
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var rectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, maxWidth), random.Next(1, maxHeight)));
                rectangles.Any(rect => rect.IntersectsWith(rectangle)).Should().BeFalse();
                rectangles.Add(rectangle);
            }

            cloudConfiguration.NeedSnuggle = false;
        }

        [TestCase(2, 2, 10, TestName = "Small rectangles")]
        [TestCase(100, 100, 10, TestName = "CommonRectangles")]
        [TestCase(100, 100, 100, TestName = "Many Rectangles")]
        public void PutNextRectangle_PlaceTightly(int maxWidth, int maxHeight, int rectanglesCount)
        {
            cloudConfiguration.NeedSnuggle = true;
            cloudConfiguration.CloudCenter = Point.Empty;

            var rectangles = new List<Rectangle>();
            var random = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var rectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, maxWidth), random.Next(1, maxHeight)));
                var deltaX = Math.Sign(rectangle.X);
                var deltaY = Math.Sign(rectangle.Y);

                var rectangleOffsetByX = new Rectangle(rectangle.Location - new Size(deltaX, 0), rectangle.Size);
                var rectangleOffsetByY = new Rectangle(rectangle.Location - new Size(0, deltaY), rectangle.Size);

                if (rectangles.Count > 0)
                    rectangles.Any(rect => rect.IntersectsWith(rectangleOffsetByX) ||
                                           rect.IntersectsWith(rectangleOffsetByY)).Should().BeTrue();

                rectangles.Add(rectangle);
            }

            cloudConfiguration.NeedSnuggle = false;
        }
    }
}
