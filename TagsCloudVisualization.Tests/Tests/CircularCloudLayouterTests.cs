using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Configurations;
using TagsCloudVisualization.Tests.Helpers;

namespace TagsCloudVisualization.Tests.Tests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private readonly string projectDirectory 
            = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        private List<Rectangle> rectangles;
        private CircularCloudLayouter cloudLayouter;
        private Point center;

        [SetUp]
        public void SetUp()
        {
            center = new Point(500, 500);
            rectangles = new List<Rectangle>();
            cloudLayouter = new CircularCloudLayouter(DistributionConfiguration.Default);
        }
        
        [Test]
        [Description("GetNextRectangle must return new rectangle with correct position")]
        public void GetNextRectangle_SuccessPath_ShouldAddNewRectangle()
        {
            var rectangleSize = new Size(100, 100);

            rectangles.Add(cloudLayouter.GetNextRectangle(center, rectangles, rectangleSize));
            
            Assert.IsNotEmpty(rectangles);
        }
        
        [Test]
        [Description("First rectangle must be located in the center")]
        public void PutNextRectangle_WithFirstRectangle_ShouldAddInCenter()
        {
            var rectangleSize = new Size(100, 100);

            rectangles.Add(cloudLayouter.GetNextRectangle(center, rectangles, rectangleSize));
            
            Assert.AreEqual(center.X - rectangleSize.Width / 2, rectangles[^1].X);
            Assert.AreEqual(center.Y - rectangleSize.Height / 2, rectangles[^1].Y);
        }
        
        [Test]
        [Description("GenerateCloud must return a list of rectangles with the length of a list of sizes")]
        public void GenerateCloud_SuccessPath_ShouldReturnAmountRectangles()
        {
            var amount = 100;
            var listSize = TagCloudHelper.GenerateRectangleSizesRandom(amount);
            
            rectangles = cloudLayouter.GenerateCloud(center, listSize);
            
            Assert.AreEqual(amount, rectangles.Count);
        }

        [Test]
        [Description("All rectangles must not intersect")]
        public void IsRectanglesIntersect_AllRectanglesNotIntersect_ShouldReturnTrue()
        {
            rectangles = cloudLayouter
                .GenerateCloud(center, TagCloudHelper.GenerateRectangleSizesRandom(50));

            for (var i = 1; i < rectangles.Count; i++)
                for (var j = 0; j < i; j++)
                    Assert.AreEqual(true, !rectangles[i].IntersectsWith(rectangles[j]));
        }

        [Test]
        [Description("All rectangles must be placed in a circle closer to the center")]
        public void CloseLocationRectangles_AllRectangleShiftToCenter_ShouldReturnTrue()
        {
            rectangles = cloudLayouter
                .GenerateCloud(center, TagCloudHelper.GenerateRectangleSizesRandom(150));

            var radius = Math.Max(rectangles.Sum(x => x.Width), rectangles.Sum(x => x.Height)) / 2;

            foreach (var rectangle in rectangles)
            {
                Assert.AreEqual(true, 
                    Math.Pow(rectangle.X - center.X, 2) + Math.Pow(rectangle.Y - center.Y, 2) < radius * radius);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome == ResultState.Failure)
            {
                var path = string.Concat(projectDirectory, $"\\FailureImages\\{TestContext.CurrentContext.Test.Name}.png");
                var bitmap = TagCloudHelper.DrawTagCloudRectangles(rectangles, CloudConfiguration.Default);
                
                bitmap.Save(path);
                
                Console.Error.WriteLine($"Tag cloud visualization saved to file {path}");
            }
        }
    }
}