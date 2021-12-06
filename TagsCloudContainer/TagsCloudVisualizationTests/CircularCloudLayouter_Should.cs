﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Extensions;
using TagsCloudVisualization.Layouters;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualizationTests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private const float Epsilon = 1e-5f;
        private CircularCloudLayouter layouter;
        private CircularCloudVisualizer visualizer;
        private readonly Point center = new (300, 400);
        private const int RectanglesCount = 100;
        private const int CloudsCount = 100;

        [SetUp]
        public void SetUp()
        {
            var pointPlacer = new Spiral(center);
            layouter = new CircularCloudLayouter(pointPlacer);
            visualizer = new CircularCloudVisualizer(800, 600, Color.Black,
                Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status is TestStatus.Failed or TestStatus.Inconclusive)
            {
                visualizer.PutRectangles(layouter.rectangles);
                var testName = TestContext.CurrentContext.Test.Name;
                Console.WriteLine($"Tags cloud visualizaton is saved to {visualizer.SaveImage(testName + ".png")}");
            }
        }

        [Test]
        public void PutRectangles_WithoutIntersections()
        {
            var rectangles = layouter
                .PutNextRectangles(Enumerable.Repeat(new SizeF(10, 10), RectanglesCount))
                .ToList(); // To avoid multiple enumeration

            rectangles
                .SelectMany(firstRect => rectangles
                    .Select(secondRect => firstRect != secondRect && firstRect.IntersectsWith(secondRect)))
                .Should()
                .NotContain(true);
        }

        [Test]
        public void PutNextRectangle_ThrowsArgumentException_WhenSizeIsNegative()
        {
            Action act = () => layouter.PutNextRectangle(new SizeF(-1, -1));

            act.Should().Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_PutFirstRectangleAtCenter_WithCustomCenter()
        {
            var rectangleSize = new SizeF(50, 50);

            var rectangle = layouter.PutNextRectangle(rectangleSize);

            rectangle.X.Should().BeApproximately(center.X - rectangleSize.Width / 2, Epsilon);
            rectangle.Y.Should().BeApproximately(center.Y - rectangleSize.Height / 2, Epsilon);
        }

        [Test]
        [Repeat(100)]
        public void PutNextRectangle_AverageRandomSizeRectanglesCloudDensity_GreaterThan60Percents()
        {
            var avgDensity = 0.0;

            for (int i = 0; i < CloudsCount; i++)
            {
                avgDensity += GetCloudDensity(Generators.RectanglesRandomSizeGenerator(RectanglesCount));
            }

            avgDensity /= CloudsCount;

            avgDensity.Should().BeGreaterThan(0.6);
        }

        [Test]
        [Repeat(100)]
        public void PutNextRectangle_AverageSameSizeRectanglesCloudDensity_GreaterThan70Percents()
        {
            var avgDensity = Generators.
                RectanglesRandomSizeGenerator(CloudsCount)
                .Sum(size => GetCloudDensity(Enumerable.Repeat(size, RectanglesCount)));

            avgDensity /= CloudsCount;

            avgDensity.Should().BeGreaterThan(0.7);
        }


        private double GetCloudDensity(IEnumerable<SizeF> rectanglesSizes)
        {
            layouter = new CircularCloudLayouter(new Spiral(center));
            
            var rectanglesArea = 0.0f;

            var lastRectangle = new RectangleF();

            foreach (var rectangleSize in rectanglesSizes)
            {
                lastRectangle = layouter.PutNextRectangle(rectangleSize);

                rectanglesArea += lastRectangle.GetArea();
            }

            var floatCenter = new PointF(center.X, center.Y);

            var circleRadius = floatCenter.DistanceTo(lastRectangle.Location);
            var circleArea = circleRadius * circleRadius * Math.PI;

            var density = rectanglesArea / circleArea;

            return density;
        }
    }
}