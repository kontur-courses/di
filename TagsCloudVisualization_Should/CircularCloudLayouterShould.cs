using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Should
{
    public class CircularCloudLayouterShould
    {
        private List<Rectangle> actualRectangles;
        private Point center;

        [Test]
        public void PutNextRectangle_ThrowArgumentException_SizeOfRectangleHaveNegativeValue()
        {
            center = new Point(100, 100);
            var pointProvider = new PointProvider(center);
            var cloud = new CircularCloudLayouter(pointProvider);

            Action act = () => cloud.PutNextRectangle(new Size(-100, -100));

            act.ShouldThrow<ArgumentException>().WithMessage("Width or height of rectangle was negative");
        }

        [Test]
        public void PutNextRectangle_ReturnSameRectangle_OneRectangle()
        {
            center = new Point(40, 40);
            var pointProvider = new PointProvider(center);
            var expectedRectangle = new Rectangle(new Point(40, 40), new Size(30, 30));
            var cloud = new CircularCloudLayouter(pointProvider);

            var actual = cloud.PutNextRectangle(new Size(30, 30));

            actual.ShouldBeEquivalentTo(expectedRectangle);

        }

        [Test]
        public void Rectangles_CountIsTen_RandomTenRectangles()
        {
            var rnd = new Random();
            center = new Point(500, 500);
            var pointProvider = new PointProvider(center);
            var cloud = new CircularCloudLayouter(pointProvider);
            const int expectedLength = 10;

            for (var i = 0; i < 10; i++)
            {
                var size = new Size(rnd.Next(10, 200), rnd.Next(10, 200));
                cloud.PutNextRectangle(size);
            }
            var actualLength = cloud.Rectangles.Count;
            actualRectangles = cloud.Rectangles;

            actualLength.Should().Be(expectedLength);
        }

        [Test]
        public void Rectangles_SameOrderLikeAdded_ThreeRectangles()
        {
            center = new Point(500, 500);
            var pointProvider = new PointProvider(center);
            var cloud = new CircularCloudLayouter(pointProvider);
            var expectedRectangles = new List<Rectangle>
            {
                new Rectangle(new Point(500, 500), new Size(30, 30)),
                new Rectangle(new Point(530, 493), new Size(40, 40)),
                new Rectangle(new Point(510, 530), new Size(20, 20))
            };

            cloud.PutNextRectangle(new Size(30, 30));
            cloud.PutNextRectangle(new Size(40, 40));
            cloud.PutNextRectangle(new Size(20, 20));
            actualRectangles = cloud.Rectangles;

            actualRectangles.ShouldAllBeEquivalentTo(expectedRectangles);

        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
            var image = Drawer.DrawImage(actualRectangles, center);
            var saver = new PngSaver();
            var name = TestContext.CurrentContext.Test.Name;
            saver.SaveImage(image, name);
        }
    }
}
