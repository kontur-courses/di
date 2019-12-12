using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudContainer.CloudLayouter;
using TagsCloudContainer.CloudLayouter.Spiral;

namespace TagsCloudContainer.Tests
{
     public class CircularCloudLayouterTests
     {
         private List<Rectangle> rectangles;
        [SetUp]
        public void SetUp()
        {
            rectangles = new List<Rectangle>();
        }
        
        [Test]
        public void Constructor_DoesNotThrow_WithСorrectСenter([ValueSource(nameof(cloudCenters))]Point center) 
        { 
            Action action = () => new CircularCloudLayouter(center, p => new CircularSpiral(p)); 
            action.Should().NotThrow(); 
        }
        
        
        [Test]
        public void PutNextRectangle_LocateFirstRectangle_OnSpecifiedByXCenter([ValueSource(nameof(cloudCenters))]Point center)
        {
            var circularCloudLayouter = new CircularCloudLayouter(center, p => new CircularSpiral(p)); 
            rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(31 , 42)));
            rectangles[0].X.Should().Be(center.X - 31 / 2);
        }
        
        [Test]
        public void PutNextRectangle_LocateFirstRectangle_OnSpecifiedByYCenter( [ValueSource(nameof(cloudCenters))]Point center)
        {
            var circularCloudLayouter = new CircularCloudLayouter(center, p => new CircularSpiral(p)); 
            rectangles.Add(circularCloudLayouter.PutNextRectangle(new Size(31 , 42)));
            rectangles[0].Y.Should().Be(center.Y - 42 / 2);
        }
        
        
        
        [TestCase(2, TestName = "TwoRectangles")]
        [TestCase(10, TestName = "TenRectangles")]
        [TestCase(20, TestName = "TwentyRectangles")]
        [TestCase(100, TestName = "HundredRectangles")]
        public void PutNextRectangle_RectanglesMustNotIntersect(int countRectangles)
        {
            var circularCloudLayouter = new CircularCloudLayouter(new Point(0, 0), p => new CircularSpiral(p));
            rectangles.AddRange(Enumerable.Range(10, countRectangles)
                .Select(i => circularCloudLayouter.PutNextRectangle(new Size(i * 3, i))));
            
            CircularCloudLayouter.HasOverlappingRectangles(rectangles).Should().BeFalse();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Failure) return;
            var path = Path.Combine(Environment.CurrentDirectory, "TagCloudTests");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var circularCloudDrawing = new CircularCloudDrawing(new Size(2000, 2000),Color.Azure, 
                p => new CircularCloudLayouter(p, p1 => new CircularSpiral(p1)));
            foreach (var rectangle in rectangles) 
                circularCloudDrawing.DrawRectangle(rectangle, new Pen(Color.Blue));
            var testName = TestContext.CurrentContext.Test.FullName;
            path =  Path.Combine(path, $"{testName}.png");
            circularCloudDrawing.SaveImage(path);
        }

        private static IEnumerable<Point> cloudCenters = Enumerable 
        .Range(-1, 3) 
        .SelectMany(i => Enumerable 
            .Range(-1, 3) 
            .Select(j => new Point(i, j))); 
    }
}