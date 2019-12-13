﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.Algorithm.SpiralBasedLayouter;
using TagCloud.Infrastructure;

namespace TagCloudTests.Algorithm.SpiralBasedLayouter
{
    public class CircularCloudLayouterTests
    {
        private List<Rectangle> rectangles;
        private PictureConfig config;

        [SetUp]
        public void SetUp()
        {
            rectangles = null;
            config = A.Fake<PictureConfig>();
            config.Size = new Size(0,0);
        }

        [Test]
        public void PutNextRectangle_ShouldPutRectangleInCenter_WhenOneRectangle()
        {
            var size = new Size(2, 1);
            var layouter = new CircularCloudLayouter(config);

            var rectangle = layouter.PutNextRectangle(size);
            rectangles = new List<Rectangle> { rectangle };

            rectangle.Should()
                .Match<Rectangle>(r =>
                    r.Top == 0 && r.Left == 0
                    || r.Top == 0 && r.Right == 0
                    || r.Bottom == 0 && r.Left == 0
                    || r.Bottom == 0 && r.Right == 0);
        }

        [Test]
        public void PutNextRectangle_RectanglesShouldNotIntersect_WhenTwoRectangles()
        {
            var firstSize = new Size(2, 1);
            var secondSize = new Size(3, 4);
            var layouter = new CircularCloudLayouter(config);

            var firstRectangle = layouter.PutNextRectangle(firstSize);
            var secondRectangle = layouter.PutNextRectangle(secondSize);
            rectangles = new List<Rectangle> { firstRectangle, secondRectangle };

            RectangleUtils.RectanglesAreIntersected(firstRectangle, secondRectangle)
                .Should()
                .BeFalse();
        }

        [TestCase(2, TestName = "2 rectangles")]
        [TestCase(3, TestName = "3 rectangles")]
        [TestCase(5, TestName = "5 rectangles")]
        [TestCase(10, TestName = "10 rectangles")]
        [TestCase(50, TestName = "50 rectangles")]
        [TestCase(100, TestName = "100 rectangles")]
        public void PutNextRectangle_RectanglesShouldNotIntersect_WhenManyRectangles(int rectanglesCount)
        {
            var size = new Size(2, 1);
            var layouter = new CircularCloudLayouter(config);

            rectangles = Enumerable.Range(0, rectanglesCount)
                .Select(n => layouter.PutNextRectangle(size)).ToList();

            rectangles
                .SelectMany((x, i) => rectangles.Skip(i + 1), RectangleUtils.RectanglesAreIntersected)
                .Should()
                .AllBeEquivalentTo(false);
        }

        [TestCase(2, TestName = "2 rectangles")]
        [TestCase(3, TestName = "3 rectangles")]
        [TestCase(5, TestName = "5 rectangles")]
        [TestCase(10, TestName = "10 rectangles")]
        [TestCase(20, TestName = "20 rectangles")]
        [TestCase(50, TestName = "50 rectangles")]
        [TestCase(100, TestName = "100 rectangles")]
        public void PutNextRectangle_ShouldPlaceRectanglesTightly_WhenManyRectanglesWithSameSize(
            int rectanglesCount)
        {
            var size = new Size(2, 1);
            var layouter = new CircularCloudLayouter(config);

            rectangles = Enumerable.Range(0, rectanglesCount)
                .Select(n => layouter.PutNextRectangle(size)).ToList();

            foreach (var rectangle in rectangles)
            {
                foreach (var closerRectangle in RectangleUtils.GetRectanglesThatCloserToPoint(
                    config.Center, rectangle, 1))
                {
                    rectangles
                        .Where(r => r != rectangle)
                        .Select(r => RectangleUtils.RectanglesAreIntersected(r, closerRectangle))
                        .Should()
                        .Contain(true);
                }
            }
        }

        [TestCase(2, TestName = "2 rectangles")]
        [TestCase(3, TestName = "3 rectangles")]
        [TestCase(5, TestName = "5 rectangles")]
        [TestCase(10, TestName = "10 rectangles")]
        [TestCase(20, TestName = "20 rectangles")]
        [TestCase(50, TestName = "50 rectangles")]
        [TestCase(100, TestName = "100 rectangles")]
        public void PutNextRectangle_ShouldPlaceRectanglesTightly_WhenManyRectanglesWithDifferentSize(int rectanglesCount)
        {
            var layouter = new CircularCloudLayouter(config);
            var sizes = new List<Size>();
            var rnd = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var width = rnd.Next(10);
                var height = rnd.Next(10);
                sizes.Add(new Size(width, height));
            }

            rectangles = sizes.Select(s => layouter.PutNextRectangle(s)).ToList();

            foreach (var rectangle in rectangles)
            {
                foreach (var closerRectangle in RectangleUtils.GetRectanglesThatCloserToPoint(
                    config.Center, rectangle, 1))
                {
                    rectangles
                        .Where(r => r != rectangle)
                        .Select(r => RectangleUtils.RectanglesAreIntersected(r, closerRectangle))
                        .Should()
                        .Contain(true);
                }
            }
        }

        [TestCase(2, TestName = "2 rectangles")]
        [TestCase(3, TestName = "3 rectangles")]
        [TestCase(5, TestName = "5 rectangles")]
        [TestCase(10, TestName = "10 rectangles")]
        [TestCase(20, TestName = "20 rectangles")]
        [TestCase(50, TestName = "50 rectangles")]
        [TestCase(100, TestName = "100 rectangles")]
        public void PutNextRectangle_ShouldPlaceRectanglesCloseToCircle_WhenManyRectanglesWithSameSize(
            int rectanglesCount)
        {
            var size = new Size(2, 1);
            var layouter = new CircularCloudLayouter(config);

            rectangles = Enumerable.Range(0, rectanglesCount)
                .Select(n => layouter.PutNextRectangle(size)).ToList();

            var expectedMaxDelta = rectangles.Select(RectangleUtils.GetRectangleDiagonal).Max();

            var convexHullPoints = GeometryUtils.BuildConvexHull(rectangles);
            var distances = convexHullPoints.Select(p => DistanceUtils.GetDistanceFromPointToPoint(p, config.Center));
            var actualDelta = distances.Max() - distances.Min();

            (expectedMaxDelta - actualDelta).Should().BeGreaterOrEqualTo(0);
        }

        [TestCase(2, TestName = "2 rectangles")]
        [TestCase(3, TestName = "3 rectangles")]
        [TestCase(5, TestName = "5 rectangles")]
        [TestCase(10, TestName = "10 rectangles")]
        [TestCase(20, TestName = "20 rectangles")]
        [TestCase(50, TestName = "50 rectangles")]
        [TestCase(100, TestName = "100 rectangles")]
        public void PutNextRectangle_ShouldPlaceRectanglesCloseToCircle_WhenManyRectanglesWithDifferentSize(
            int rectanglesCount)
        {
            var layouter = new CircularCloudLayouter(config);
            var sizes = new List<Size>();
            var rnd = new Random();
            for (var i = 0; i < rectanglesCount; i++)
            {
                var width = rnd.Next(10);
                var height = rnd.Next(10);
                sizes.Add(new Size(width, height));
            }

            rectangles = sizes.Select(s => layouter.PutNextRectangle(s)).ToList();

            var expectedMaxDelta = rectangles.Select(RectangleUtils.GetRectangleDiagonal).Max();

            var convexHullPoints = GeometryUtils.BuildConvexHull(rectangles);
            var distances = convexHullPoints.Select(p => DistanceUtils.GetDistanceFromPointToPoint(p, config.Center));
            var actualDelta = distances.Max() - distances.Min();

            (expectedMaxDelta - actualDelta).Should().BeGreaterOrEqualTo(0);
        }
        
        [TearDown]
        public void TearDown()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Outcome.Status != TestStatus.Failed)
                return;

            var testMethodName = TestContext.CurrentContext.Test.MethodName;
            var testName = TestContext.CurrentContext.Test.Name;
            var fileName = $"{AppDomain.CurrentDomain.BaseDirectory}\\fail_{testMethodName}_{testName}.bmp";

            var visualizer = new Visualizer(config.Center, 10);
            var bitmap = visualizer.GetVisualization(rectangles);
            bitmap.Save(fileName);

            TestContext.Out.WriteLine($"Tag cloud visualization saved to file {fileName}");
        }
    }
}