using System;
using System.Collections.Generic;
using System.Drawing;
using NUnit.Framework;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization
{
    [TestFixture]
    class CircularCloudLayouter_should
    {
        static CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void SetUp() => cloudLayouter = new CircularCloudLayouter(new Point(50, 50), new RectangularSpiral());

        [TearDown]
        public void CreateDebugImage()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;

            var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{TestContext.CurrentContext.Test.Name}.png");

            var canvas = new Canvas(100,100);
            foreach (var rec in cloudLayouter.GetAllRectangles())
                canvas.Draw(rec, new SolidBrush(Color.DarkRed));
            canvas.Save(imagePath);
        }

        [TestCase(-1, 1)]
        [TestCase(1, -1)]
        [TestCase(-1, -1)]
        public void PutNextRectangle_ThrowArgumentException_WhenSizeParamsIsNegative(int width, int height)
        {
            Action act = () => cloudLayouter.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        [TestCase(0, 0)]
        public void PutNextRectangle_ThrowArgumentException_WhenSizeIsZero(int width, int height)
        {
            Action act = () => cloudLayouter.PutNextRectangle(new Size(width, height));
            act.Should().Throw<ArgumentException>();
        }

        [TestCase(5, 5)]
        [TestCase(7, 2)]
        [TestCase(100, 123)]
        public void PutNextRectangle_ShouldReturnRectangle_WithGivenSize(int width, int height)
        {
            var rectangle = cloudLayouter.PutNextRectangle(new Size(width, height));

            rectangle.Size.Should().Be(new Size(width, height));
        }

        [TestCase(0, 0)]
        [TestCase(2, 3)]
        [TestCase(-4, 1)]
        public void PutNextRectangle_ShouldBePutFirstRectangle_InCenter(int centerX, int centerY)
        {
            var center = new Point(centerX, centerY);
            var cloudLayouter = new CircularCloudLayouter(center, new RectangularSpiral());

            var firstRectangle = cloudLayouter.PutNextRectangle(new Size(1, 1));

            firstRectangle.Location.Should().Be(center);
        }

        [TestCase(1, 1, 1, 1, Description = "The first and second rectangle are squares")]
        [TestCase(5, 10, 10, 5, Description = "First rectangle size = (W:5, H:10), second rectangle size = (W:10, H:5)")]
        [TestCase(10, 5, 5, 10, Description = "first rectangle size = (W:10, H:5), second rectangle size = (W:5, H:10)")]
        // Добавил для верхних ТестКейсов такое описани, так как решил что иное просто запутает людей.
        [TestCase(100, 100, 1, 1, Description = "First rectangle is larger than second")]
        [TestCase(1, 1, 100, 100, Description = "Second rectangle is larger than first")]
        public void PutNextRectangle_FirstAndSecondRectangles_Should_NotIntersect(int rect1Width, int rect1Height, int rect2Width, int rect2Height)
        {
            var firstRectangle = cloudLayouter.PutNextRectangle(new Size(rect1Width, rect1Height));
            var secondRectangle = cloudLayouter.PutNextRectangle(new Size(rect2Width, rect2Height));

            if (firstRectangle.IntersectsWith(secondRectangle))
                Assert.Fail($"rectangle: {firstRectangle} is intersect with rectangle {secondRectangle}");
        }

        [Category("long tests")]
        [TestCase(10)]
        [TestCase(25)]
        [TestCase(50)]
        [TestCase(100)]
        public void PutNextRectangle_AllRectanglesOnOneCloud_Should_NotIntersect(int countRectangles)
        {
            var random = new Random();
            var rectangles = new List<Rectangle>();

            for (int stage = 0; stage < 50; stage++) // каждое countRectangle проверяется в 50 проходов
            {
                for (int i = 0; i < countRectangles; i++)
                {
                    var size = new Size(random.Next(10, 20), random.Next(10, 20));
                    rectangles.Add(cloudLayouter.PutNextRectangle(size));
                }

                for (int i = 0; i < rectangles.Count; i++)
                    for (int j = i + 1; j < rectangles.Count; j++)
                        if (rectangles[i].IntersectsWith(rectangles[j]))
                            Assert.Fail($"rectangle: {rectangles[i]} is intersect with rectangle {rectangles[j]}");
            }
        }

        [Category("long tests")]
        [TestCase(25)]
        [TestCase(50)]
        [TestCase(100)]
        public void PutNextRectangle_ResultingCloud_ShouldBe_Round(int countRectangles)
        {
            var random = new Random();

            for (int stage = 0; stage < 50; stage++)
            {
                for (int i = 0; i < countRectangles; i++)
                {
                    var size = new Size(random.Next(10, 20), random.Next(10, 20));
                    cloudLayouter.PutNextRectangle(size);
                }

                double radius = cloudLayouter
                    .GetAllRectangles()
                    .Select(rec => GetDistance(rec, cloudLayouter.Center))
                    .OrderByDescending(distance => distance)
                    .First();

                double widthCloud = cloudLayouter.GetAllRectangles().Max(rec => rec.X) -
                                 cloudLayouter.GetAllRectangles().Min(rec => rec.X);

                double heightCloud = cloudLayouter.GetAllRectangles().Max(rec => rec.Y) -
                                  cloudLayouter.GetAllRectangles().Min(rec => rec.Y);

                (radius / (widthCloud / 2)).Should().BeLessThan(1.5);
                (radius / (heightCloud / 2)).Should().BeLessThan(1.5);
            }
        }

        [Category("long tests")]
        [TestCase(25)]
        [TestCase(50)]
        [TestCase(100)]
        public void PutNextRectangle_ResultingCloud_ShouldBe_Tight(int countRectangles)
        {
            var random = new Random();

            for (int stage = 0; stage < 50; stage++)
            {
                for (int i = 0; i < countRectangles; i++)
                {
                    var size = new Size(random.Next(10, 20), random.Next(10, 20));
                    cloudLayouter.PutNextRectangle(size);
                }

                var radius = cloudLayouter
                    .GetAllRectangles()
                    .Select(rec => GetDistance(rec, cloudLayouter.Center))
                    .OrderBy(distance => distance)
                    .First();
                var occupiedArea = cloudLayouter
                    .GetAllRectangles()
                    .Select(a => a.Width * a.Height)
                    .Sum();
                var cloudArea = Math.PI * radius * radius;

                (cloudArea / occupiedArea).Should().BeLessThan(2);
            }
        }

        double GetDistance(Rectangle rec, Point point) => 
            Math.Sqrt(Math.Pow(rec.X - point.X, 2) + Math.Pow(rec.Y - point.Y, 2));
    }
}
