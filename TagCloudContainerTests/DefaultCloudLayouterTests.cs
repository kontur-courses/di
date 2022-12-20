using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer;
using TagsCloudContainer.LayouterAlgorithms;
using TagsCloudContainer.UI;

namespace TagCloudContainerTests
{
    public class DefaultCloudLayouterTests
    {
        private CircularCloudLayouter layouter;
        private Point center;
        private List<Rectangle> rectangles;
        private ConsoleUiSettings settings;

        [SetUp]
        public void SetUp()
        {
            settings = Parser.Default.ParseArguments<ConsoleUiSettings>(new string[] { }).Value;
            settings.AngleOffset = 1;
            settings.RadiusOffset = 1;
            center = new Point(settings.CanvasWidth / 2, settings.CanvasHeight / 2);
            layouter = new CircularCloudLayouter(new Spiral(settings));
            rectangles = new List<Rectangle>();
        }

        [Test]
        public void PutNextRectangle_ShouldReturnRectangleWithCorrectSize()
        {
            var size = new Size(1000, 10000);
            var rectangle = layouter.PutNextRectangle(size);
            rectangle.Size.Should().Be(size);
        }

        [Test]
        public void PutNextRectangle_ShouldPlaceFirstRectangleInCenter()
        {
            var size = new Size(8, 8);
            var rectangleInCenter = new Rectangle(new Point(center.X - 3, center.Y - 3), size);
            var rectangle = layouter.PutNextRectangle(size);
            rectangle.Should().BeEquivalentTo(rectangleInCenter);
        }

        [TestCase(1, -3, TestName = "Y < 0")]
        [TestCase(-3, 1, TestName = "X < 0")]
        [TestCase(-3, -3, TestName = "X < 0, Y < 0")]
        public void PutNextRectangle_ShouldThrowArgumentException_OnIncorrectInput(int x, int y)
        {
            var size = new Size(x, y);
            Action act = () => layouter.PutNextRectangle(size);
            act.Should().Throw<ArgumentException>().WithMessage("Wrong size of rectangle");
        }

        [TestCase(0, 3, TestName = "X = 0, Y > 0")]
        [TestCase(3, 0, TestName = "X > 0, Y = 0")]
        [TestCase(0, 0, TestName = "X = 0, Y = 0")]
        [TestCase(10000, 10000, TestName = "Big rectangle")]
        public void PutNextRectangle_ShouldReturnCorrectRectangle_OnCorrectInput(int x, int y)
        {
            var size = new Size(x, y);
            var coordinatesCorrectRectangle = new Point(center.X - x / 2 + 1, center.Y - y / 2 + 1);
            var rectangle = layouter.PutNextRectangle(size);
            rectangle.Should().BeEquivalentTo(new Rectangle(coordinatesCorrectRectangle, size));
        }

        [Test]
        public void PutNextRectangle_ShouldNotIntersectRectangles()
        {
            var rnd = new Random();
            for (var i = 0; i < 100; i++)
            {
                var rectangle = layouter.PutNextRectangle(new Size(rnd.Next(10, 100), rnd.Next(10, 50)));
                rectangles.Add(rectangle);
            }

            foreach (var rectangle in rectangles)
                rectangles.Any(rect => rect.IntersectsWith(rectangle) && rect != rectangle).Should().BeFalse();
        }

        [TestCaseSource(nameof(_putNextRectangleShouldCorrectPlaceRectanglesCases))]
        public void PutNextRectangle_ShouldCorrectPlaceRectangles(List<Rectangle> expectedRectangles)
        {
            var rectangleSize = new Size(10, 10);
            for (var i = 0; i < 5; i++)
            {
                var rectangle = layouter.PutNextRectangle(rectangleSize);
                rectangles.Add(rectangle);
            }

            for (var i = 0; i < 5; i++)
                rectangles[i].Location.Should().BeEquivalentTo(expectedRectangles[i].Location);
        }

        private static object[] _putNextRectangleShouldCorrectPlaceRectanglesCases =
        {
            new List<Rectangle>
            {
                new Rectangle(496, 496, 10, 10),
                new Rectangle(495, 484, 10, 10),
                new Rectangle(507, 500, 10, 10),
                new Rectangle(497, 509, 10, 10),
                new Rectangle(484, 505, 10, 10)
            }
        };
    }
}