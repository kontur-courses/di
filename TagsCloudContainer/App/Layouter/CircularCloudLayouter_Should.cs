using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudContainer.App.Layouter
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        //    private CircularCloudLayouter cloudLayouter;

        //    [SetUp]
        //    public void SetUp()
        //    {
        //        cloudLayouter = new CircularCloudLayouter(new Point(400, 250));
        //    }

        //    [Test]
        //    public void CircularCloudLayouter_ShouldThrowArgumentException_WhenNegativeX_Y()
        //    {
        //        Action act = () =>
        //        {
        //            var circularCloudLayouter = new CircularCloudLayouter(new Point(-1, -1));
        //        };
        //        act.Should().Throw<ArgumentException>();
        //    }

        //    [Test]
        //    public void PutNextRectangle_ShouldReturnRectangleInCenter_WhenOneRectangle()
        //    {
        //        var center = cloudLayouter.Center;
        //        var rectangleSize = new Size(200, 30);
        //        var point = new Point(center.X - rectangleSize.Width / 2, center.Y - rectangleSize.Height / 2);
        //        var expectedRect = new Rectangle(point, rectangleSize);

        //        var rect = cloudLayouter.PutNextRectangle(rectangleSize);

        //        rect.Should().BeEquivalentTo(expectedRect);
        //    }

        //    [TestCase(true)]
        //    [TestCase(false)]
        //    public void PutNextRectangle_ShouldReturnIntersectedRectangles(bool input)
        //    {
        //        if (input)
        //            cloudLayouter.IsOffsetToCenter = true;
        //        cloudLayouter.PutNextRectangle(new Size(300, 100));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(50, 52));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 21));

        //        cloudLayouter.Rectangles.AreIntersected().Should().BeFalse();
        //    }

        //    [TestCase(true)]
        //    [TestCase(false)]
        //    public void PutNextRectangle_ShouldArrangeRectanglesInShapeCircle(bool input)
        //    {
        //        if (input)
        //            cloudLayouter.IsOffsetToCenter = true;
        //        var center = cloudLayouter.Center;
        //        cloudLayouter.PutNextRectangle(new Size(300, 100));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(50, 52));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 21));

        //        var distanceToExtremePoints = new List<int>();
        //        distanceToExtremePoints.Add(center.X - cloudLayouter.Rectangles.Min(x => x.Left));
        //        distanceToExtremePoints.Add(cloudLayouter.Rectangles.Max(x => x.Right) - center.X);
        //        distanceToExtremePoints.Add(center.Y - cloudLayouter.Rectangles.Min(x => x.Top));
        //        distanceToExtremePoints.Add(cloudLayouter.Rectangles.Max(x => x.Bottom) - center.Y);

        //        var avr = distanceToExtremePoints.Average();
        //        var distMoreAvr = distanceToExtremePoints.Where(x => x > 1.2 * avr || x < 0.8 * avr);
        //        distMoreAvr.Count()
        //            .Should().Be(0, "расстояния до крайних точек не должны отличаться от среднего больше, чем на 20%");
        //        //если поменять строки на закоменченные, можно проверить, что задача 3 работает
        //        //var distMoreAvr = distanceToExtremePoints.Where(x => x > 1.1 * avr || x < 0.9 * avr);
        //        //distMoreAvr.Count()
        //        //    .Should().Be(0, "расстояния до крайних точек не должны отличаться от среднего больше, чем на 10%");
        //    }

        //    [TestCase(true)]
        //    [TestCase(false)]
        //    //[Ignore("Ignore a test")]
        //    public void CreateNewImageCloudLayouter(bool input)
        //    {
        //        if (input)
        //            cloudLayouter.IsOffsetToCenter = true;
        //        cloudLayouter.PutNextRectangle(new Size(300, 100));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(50, 52));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 21));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 100));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(100, 31));
        //        cloudLayouter.PutNextRectangle(new Size(100, 30));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));
        //        cloudLayouter.PutNextRectangle(new Size(50, 20));

        //        cloudLayouter.SaveBitmap(TestContext.CurrentContext.Test.Name);
        //    }

        //    [TearDown]
        //    public void TearDown()
        //    {
        //        var testResult = TestContext.CurrentContext.Result.Outcome;

        //        if (Equals(testResult, ResultState.Failure) ||
        //            Equals(testResult == ResultState.Error))
        //        {
        //            cloudLayouter.SaveBitmap(TestContext.CurrentContext.Test.Name);
        //            Console.WriteLine("Tag cloud visualization saved to file " + Environment.CurrentDirectory 
        //                                                                       + "\\" + TestContext.CurrentContext.Test.Name+ ".bmp");
        //        }
        //    }

    }
}
