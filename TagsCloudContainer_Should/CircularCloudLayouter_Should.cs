using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.App.Layouter;

namespace TagsCloudContainerTests
{
    public class CircularCloudLayouter_Should
    {
        [Test]
        public void CircularCloudLayouter_ShouldThrowArgumentException_WhenNegativeX_Y()
        {
            var cloudLayouterSettings = new CloudLayouterSettings();
            cloudLayouterSettings.Center = new Point(-1, -1);
            Action act = () =>
            {
                var circularCloudLayouter = new CircularCloudLayouter(cloudLayouterSettings);
            };
            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectangleInCenter_WhenOneRectangle()
        {
            var cloudLayouterSettings = new CloudLayouterSettings();
            var center = cloudLayouterSettings.Center;
            var cloudLayouter = new CircularCloudLayouter(cloudLayouterSettings);
            var rectangleSize = new Size(200, 30);
            var point = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2);
            var expectedRect = new Rectangle(point, rectangleSize);

            var rect = cloudLayouter.PutNextRectangle(rectangleSize);

            rect.Should().BeEquivalentTo(expectedRect);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void PutNextRectangle_ShouldReturnIntersectedRectangles(bool input)
        {
            var cloudLayouterSettings = new CloudLayouterSettings();
            if (input)
                cloudLayouterSettings.IsOffsetToCenter = true;
            var cloudLayouter = new CircularCloudLayouter(cloudLayouterSettings);
            cloudLayouter.PutNextRectangle(new Size(300, 100));
            cloudLayouter.PutNextRectangle(new Size(100, 31));
            cloudLayouter.PutNextRectangle(new Size(50, 52));
            cloudLayouter.PutNextRectangle(new Size(100, 31));
            cloudLayouter.PutNextRectangle(new Size(100, 30));
            cloudLayouter.PutNextRectangle(new Size(100, 30));
            cloudLayouter.PutNextRectangle(new Size(100, 30));
            cloudLayouter.PutNextRectangle(new Size(50, 21));

            cloudLayouter.Rectangles.AreIntersected().Should().BeFalse();
        }


    }
}
