using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization;

namespace TagCloudTests
{
    [TestFixture]
    public class CircularCloudMakerTests
    {
        private CircularCloudMaker maker;
        
        private static bool AreContacts(RectangleF rectangle, RectangleF other, double delta)
        {
            return Math.Abs(rectangle.Top - other.Top) < delta && (Math.Abs(rectangle.Left - other.Right) < delta ||
                                                                       Math.Abs(rectangle.Right - other.Left) < delta) ||
                   Math.Abs(rectangle.Left - other.Left) < delta && (Math.Abs(rectangle.Top - other.Bottom) < delta ||
                                                                         Math.Abs(rectangle.Bottom - other.Top) < delta);
        }

        [SetUp]
        public void SetUp()
        {
            maker = new CircularCloudMaker(new Point());
        }
        
        [TearDown]
        public void ShowFailedCloud()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
            var image = RectangleCloudVisualiser.DrawCloud(maker, new Size(800, 800));
            var fileName = TestContext.CurrentContext.Test.Name + "Fail.png";
            image?.Save(fileName, ImageFormat.Png);
            Console.WriteLine("Failed test saved at " + Directory.GetCurrentDirectory() + "\\" + fileName);
        }
        
        [Test]
        public void PutRectangle_PlacesRectangle()
        {
            maker.PutRectangle(new Size(2, 2));
            maker.Rectangles.Should().HaveCount(1);
        }

        [Test]
        public void PutRectangle_FirstRectangle_InCenter()
        {
            var rect = maker.PutRectangle(new Size(2, 2));
            var expected = new RectangleF(-1, -1, 2, 2);
            rect.Should().Be(expected);
        }
        
        [Test]
        public void PutRectangle_PlaceTwoRectangles()
        {
            maker.PutRectangle(new Size(2, 2));
            maker.PutRectangle(new Size(2, 2));
            maker.Rectangles.Should().HaveCount(2);
        }
        
        [Test]
        public void PutRectangle_SecondRectangle_ContactsFirst()
        {
            var rect1 = maker.PutRectangle(new Size(2, 2));
            var rect2 = maker.PutRectangle(new Size(2, 2));
            AreContacts(rect1, rect2, 0.01).Should().BeTrue();
        }

        [Test]
        public void PutRectangle_SecondRectangle_OnCloseSide()
        {
            maker.PutRectangle(new Size(4, 2));
            var rect2 = maker.PutRectangle(new Size(2, 2));
            rect2.X.Should().Be(-1);
        }

        [Test]
        public void PutRectangle_SecondAndThirdRectangle_ShouldBeDifferent()
        {
            maker.PutRectangle(new Size(2, 2));
            var rect2 = maker.PutRectangle(new Size(2, 2));
            var rect3 = maker.PutRectangle(new Size(2, 2));
            rect2.Should().NotBe(rect3);
        }
        
        [Test]
        public void PutRectangle_LongFirst_SecondAndThirdRectangleOnDifferentSides()
        {
            maker.PutRectangle(new Size(4, 2));
            var rect2 = maker.PutRectangle(new Size(2, 2));
            var rect3 = maker.PutRectangle(new Size(2, 2));
            (rect2.Y *  rect3.Y).Should().BeNegative();
        }

        [Test]
        public void PutRectangle_RectangleCanBePlacedInCorner()
        {
            for(var i = 0; i < 6; i++) maker.PutRectangle(new Size(2, 2));
            var middleRectangle = maker.Rectangles[0];
            var cornerRectangle = maker.Rectangles[5];
            middleRectangle.Should().NotBe(cornerRectangle);
        }

        [Test]
        public void PutRectangle_RectanglesNotIntersecting()
        {
            maker.PutRectangle(new Size(10, 10));
            maker.PutRectangle(new Size(20, 10));
            maker.PutRectangle(new Size(10, 20));
            foreach (var rect1 in maker.Rectangles)
            {
                foreach (var rect2 in maker.Rectangles)
                {
                    if (rect1 == rect2) continue;
                    if (rect1.IntersectsWith(rect2))
                        Assert.Fail();
                }
            }
        }

        [Test]
        public void PutRectangle_RectangleCanBePlacedInsideHole()
        {
            maker.PutRectangle(new Size(2, 2));
            maker.PutRectangle(new Size(6, 2));
            maker.PutRectangle(new Size(6, 2));
            maker.PutRectangle(new Size(2, 6));
            maker.PutRectangle(new Size(2, 6));
            maker.PutRectangle(new Size(2, 2));
            var expectedCoords = new []{new PointF(-3, -1), new PointF(1, -1)};
            expectedCoords.Contains(maker.Rectangles[^1].Location).Should().BeTrue();
        }
    }
}