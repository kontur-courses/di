using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudVisualization
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private Point center = new Point(500, 500);
        private CircularCloudLayouter circularCloudLayouter;        

        [SetUp]
        public void SetUp()
        {
            circularCloudLayouter = new CircularCloudLayouter(center);
        }

        [TestCase(-1, 1, TestName = "width less than zero")]
        [TestCase(1, -1, TestName = "height less than zero")]
        public void PutNextRectangle_ThrowsArgumentExceptionWhen
            (int width, int height)
        {
            Action ctorInvocation = () => circularCloudLayouter.PutNextRectangle(new Size(width, height));
            ctorInvocation.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_ReturnRectangleOfSameSize()
        {
            var size = new Size(100, 20);
            circularCloudLayouter.PutNextRectangle(size).Size.Should().Be(size);
        }

        [Test]
        public void PutNextRectangle_OneRectangle_PlaceToCenter()
        {
            var rectangle = circularCloudLayouter.PutNextRectangle(new Size(100, 20));
            rectangle.IntersectsWith(new Rectangle(center, new Size(1, 1))).Should().BeTrue();
        }

        [Test]
        public void PutNextRectangle_TwoRectangles_RectanglesNotIntersect()
        {
            var firstNextRectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 20));
            var secondNextRectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 20));

            firstNextRectangle.IntersectsWith(secondNextRectangle).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_ManyRectangles_RectanglesShouldNotIntersect()
        {
            var rects = new List<Rectangle>();
            for (var i = 0; i < 10; i++)
            {
                var newRectangle = circularCloudLayouter.PutNextRectangle(new Size(10, 20));
                rects.All(r => !r.IntersectsWith(newRectangle)).Should().BeTrue();
                rects.Add(newRectangle);
            }
        }

        [Test]
        public void Сloud_ShouldHasShapeOfCircle()
        {
            var rects = new List<Rectangle>();
            for (var i = 0; i < 10; i++)
            {
                rects.Add(circularCloudLayouter.PutNextRectangle(new Size(10, 20)));
            }

            var maxDistance = 0.0;
            foreach (var rect in rects)
            {
                rect.GetVertexCoordinates().ToList().ForEach(p => maxDistance = Math.Max(maxDistance,
                    p.CalculateDistanceToPoint(center)));
            }

            const double maxRectSize = 20.0;
            const int count = 10;
            const double expectedDensity = 0.5;
            maxDistance.Should().BeLessThan(maxRectSize * count * expectedDensity);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var image = RectanglesRenderer.GenerateImage(circularCloudLayouter.Rectangles);
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}/" +
                           $"{TestContext.CurrentContext.Test.FullName}.jpg";
                image.Save(path);
                Console.WriteLine($"Tag cloud visualization saved to file <{path}>");
            }
        }
    }
    
}