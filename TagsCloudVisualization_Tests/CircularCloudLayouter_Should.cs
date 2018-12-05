using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudVisualization_Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private CircularCloudLayouter cloudLayouter;
        private Point center;
        private Spiral spiral;
        private Size defaultSize;
        private List<Rectangle> rectangles;

        [SetUp]
        public void SetUp()
        {
            center = new Point(0,0);
            spiral = new Spiral(0.0005, 0);
            cloudLayouter = new CircularCloudLayouter(new LayouterSettings(center, spiral));
            defaultSize = new Size(200, 100);
            rectangles = new List<Rectangle>();
        }

        [TearDown]
        public void TearDown()
        {
            var testContext = TestContext.CurrentContext;
            var filename = $"{testContext.WorkDirectory}/{testContext.Test.Name}.png";
            if (testContext.Result.FailCount != 0)
            {
                var size = new Size(cloudLayouter.Radius * 2, cloudLayouter.Radius * 2);
                new CircularCloudVisualizer(new Palette(Color.DimGray, Brushes.FloralWhite), size)
                    .Draw(rectangles)
                    .Save(filename);
                TestContext.WriteLine($"Tag cloud visualization saved to file {filename}");
            }
        }

        [Test]
        public void SetValidCenterPoint_AfterCreation() => cloudLayouter.Center.Should().BeEquivalentTo(center);
        
        [Test]
        public void BeEmpty_AfterCreation() => cloudLayouter.Radius.Should().Be(0);

        [Test]
        public void PutNextRectangle_ThrowArgumentException_OnInvalidSize()
        {
            var size = new Size(-1, -1);
            cloudLayouter.Invoking(obj => obj.PutNextRectangle(size)).Should().Throw<ArgumentException>().WithMessage("*?positive");
        }

        [Test]
        public void PutNextRectangle_PutSingleRectangleInCenter_SingleRectangleCenterShifted()
        {
            var expectedRectangle = new Rectangle(new Point(-100, -50), defaultSize);
            cloudLayouter.PutNextRectangle(defaultSize).Should().BeEquivalentTo(expectedRectangle);
        }

        [Test]
        public void PutNextRectangle_PutTwoRectangles_RectanglesDoNotIntersect()
        {
            var random = new Random();
            rectangles.Add(cloudLayouter.PutNextRectangle(new Size(random.Next(1, 200), random.Next(1, 200))));
            cloudLayouter.PutNextRectangle(defaultSize).IntersectsWith(rectangles[0]).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_PutSeveralRectangles_RectanglesDoNotIntersect()
        {
            var random = new Random();
            for (var i = 0; i < 500; i++)
            {
                var nextRectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, 200), random.Next(1, 200)));
                rectangles.Any(nextRectangle.IntersectsWith).Should().BeFalse();
                rectangles.Add(nextRectangle);
            }
        }

        [Test]
        public void GetSurroundingCircleRadius_OnSingleRectangle_ReturnCorrectRadius()
        {
            var rectangle = cloudLayouter.PutNextRectangle(defaultSize);
            var expectedRadius = new Point(MathHelper.MaxAbs(rectangle.Left, rectangle.Right),
                MathHelper.MaxAbs(rectangle.Top, rectangle.Bottom)).GetDistanceTo(center);
            cloudLayouter.Radius.Should().Be(expectedRadius);
        }
        [Test]
        public void GetSurroundingCircleRadius_OnTwoRectangles_ReturnCorrectRadius()
        {
            cloudLayouter.PutNextRectangle(defaultSize);
            var rectangle = cloudLayouter.PutNextRectangle(defaultSize);
            var expectedRadius = new Point(MathHelper.MaxAbs(rectangle.Left, rectangle.Right),
                MathHelper.MaxAbs(rectangle.Top, rectangle.Bottom)).GetDistanceTo(center);
            cloudLayouter.Radius.Should().Be(expectedRadius);
        }

        [Test]
        public void GetSurroundingCircleRadius_OnSeveralRectangles_ReturnCorrectRadius()
        {
            for (var i=0; i < 199; i++)
                cloudLayouter.PutNextRectangle(defaultSize);
            var rectangle = cloudLayouter.PutNextRectangle(defaultSize);
            var expectedRadius = new Point(MathHelper.MaxAbs(rectangle.Left, rectangle.Right), 
                    MathHelper.MaxAbs(rectangle.Top, rectangle.Bottom))
                .GetDistanceTo(center);
            expectedRadius.Should().Be(cloudLayouter.Radius);
        }

        [Test]
        public void PutNextRectangle_PutsRectanglesTightEnough()
        {
            var totalCloudArea = 0;
            var random = new Random();
            for (var i = 0; i < 500; i++)
            {
                var nextRectangle = cloudLayouter.PutNextRectangle(new Size(random.Next(1, 200), random.Next(1, 200)));
                totalCloudArea += nextRectangle.Width * nextRectangle.Height;
            }

            var totalCircleCloudRadius = cloudLayouter.Radius;
            var totalCircleCloudArea = Math.PI * Math.Pow(totalCircleCloudRadius, 2);
            totalCircleCloudArea.Should().BeGreaterOrEqualTo(totalCloudArea);
        }
    }
}
