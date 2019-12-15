using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace TagsCloudTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        private Point CloudCenter;
        private CircularCloudLayouter.CircularCloudLayouter Layouter;
        private double GetDistance(Rectangle rectangle, Point center)
        {
            var rectangleCenter = new Point(rectangle.X + rectangle.Width / 2, rectangle.Y + rectangle.Height / 2);
            return Distance(rectangleCenter, center);
        }

        private double Distance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private void SavePicture(List<Rectangle> rectangles, string testName)
        {
            string fileName = string.Format("{0}.png", testName);
            string pictureName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", fileName);
            var imageSize = new Size(600, 600);
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            var rectBrush = new SolidBrush(Color.LightGreen);
            var rectBorderPen = new Pen(Color.Black, 2);
            foreach (var rect in rectangles)
            {
                var offsetRect = new Rectangle(new Point(rect.Location.X + imageSize.Width / 2, rect.Location.Y + imageSize.Height / 2), rect.Size);
                graphics.FillRectangle(rectBrush, offsetRect);
                graphics.DrawRectangle(rectBorderPen, offsetRect);
            }
            bitmap.Save(pictureName, ImageFormat.Png);
        }

        private void WriteLog(List<Rectangle> rectangles, string testName)
        {
            string fileName = string.Format("{0}.txt", testName);
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\", fileName);
            if (File.Exists(filePath) != true)
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Create, FileAccess.Write)))
                {
                    foreach (var rect in rectangles)
                        sw.WriteLine(rect.ToString());
                    sw.WriteLine("");
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(new FileStream(filePath, FileMode.Open, FileAccess.Write)))
                {
                    foreach (var rect in rectangles)
                        sw.WriteLine(rect.ToString());
                    sw.WriteLine("");
                }
            }
        }

        [SetUp]
        public void SetUp()
        {
            CloudCenter = new Point(0, 0);
            Layouter = new CircularCloudLayouter.CircularCloudLayouter(CloudCenter);
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
            {
                var testName = TestContext.CurrentContext.Test.Name;
                SavePicture(Layouter.GetRectangles(), testName);
                WriteLog(Layouter.GetRectangles(), testName);
            }
        }

        [Test]
        public void PutNextRectangle_PuttingOnce_RectangleCenterShouldBeInLayouterCenter()
        {
            var rnd = new Random();
            var size = new Size(rnd.Next(5, 100), rnd.Next(5, 100));
            var expectedLocation = new Point(-(int)Math.Floor(size.Width / (double)2), -(int)Math.Floor(size.Height / (double)2));

            var rect = Layouter.PutNextRectangle(size);

            rect.Location.Should().Be(expectedLocation);
        }

        [Test]
        public void PutNextRectangle_PuttingTwoRandomRectangles_RectanglesShouldNotIntersect()
        {
            var rnd = new Random();
            var size1 = new Size(rnd.Next(5, 100), rnd.Next(5, 100));
            var size2 = new Size(rnd.Next(5, 100), rnd.Next(5, 100));

            var rect1 = Layouter.PutNextRectangle(size1);
            var rect2 = Layouter.PutNextRectangle(size2);

            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_AddingThreeRectangles_ShouldNotIntersect()
        {
            var rectSize = new Size(10, 10);

            var rect1 = Layouter.PutNextRectangle(rectSize);
            var rect2 = Layouter.PutNextRectangle(rectSize);
            var rect3 = Layouter.PutNextRectangle(rectSize);

            rect3.IntersectsWith(rect2).Should().BeFalse();
            rect3.IntersectsWith(rect1).Should().BeFalse();
            rect2.IntersectsWith(rect1).Should().BeFalse();
        }


        [Test]
        public void PutNextRectangle_AddingSquareAndBiggerSquare_SquaresShouldNotIntersect()
        {
            var rect1 = Layouter.PutNextRectangle(new Size(10, 10));
            var rect2 = Layouter.PutNextRectangle(new Size(20, 20));

            rect1.IntersectsWith(rect2).Should().BeFalse();
        }

        [Test]
        public void PutNextRectangle_RandomRectangles_RectanglesShouldNotIntersect()
        {
            var rectangles = new List<Rectangle>{
            Layouter.PutNextRectangle(new Size(57, 40)),
            Layouter.PutNextRectangle(new Size(41, 24)),
            Layouter.PutNextRectangle(new Size(34, 54)),
            Layouter.PutNextRectangle(new Size(57, 48)),
            Layouter.PutNextRectangle(new Size(90, 52))
            };

            foreach (var rect1 in rectangles)
                foreach (var rect2 in rectangles)
                    if (rect1 != rect2)
                        rect1.IntersectsWith(rect2).Should().BeFalse();
        }


        [Test]
        public void PutNextRectangle_NewRectangleTouchesPreviousInTwoPlaces_LastRectangleShouldNotIntersectWithOthers()
        {
            var rects = new List<Rectangle> {
            Layouter.PutNextRectangle(new Size(10,10)),
            Layouter.PutNextRectangle(new Size(40, 10)),
            Layouter.PutNextRectangle(new Size(40, 10)),
            Layouter.PutNextRectangle(new Size(20, 100))
            };

            var testedRect = Layouter.PutNextRectangle(new Size(15, 15));

            rects.ForEach(x => testedRect.IntersectsWith(x).Should().BeFalse());
        }


        [Test]
        public void PutNextRectangle_AddingFiftyRandomRectangles_CheckDensity()
        {
            var rectangles = new List<Rectangle>();
            var rnd = new Random();
            var minRectSize = 5;
            var maxRectSize = 100;
            for (int i = 0; i < 50; i++)
            {
                var size = new Size(rnd.Next(minRectSize, maxRectSize), rnd.Next(minRectSize, maxRectSize));
                var rect = Layouter.PutNextRectangle(size);
                rectangles.Add(rect);
            }

            var cloudRadius = rectangles.Select(x => GetDistance(x, CloudCenter)).OrderByDescending(x => x).First();
            var rectanglesArea = rectangles.Select(x => x.Width * x.Height).Sum();
            var cloudArea = Math.PI * cloudRadius * cloudRadius;

            (cloudArea / rectanglesArea).Should().BeLessThan(Math.PI / 2);//площадь вписанного квадрата/площадь описанной окружности = Pi/2

        }

        [Test]
        public void PutNextRectangle_HundredRectangles_ShouldNotIntersect()
        {
            var rectangles = new List<Rectangle>();
            var rnd = new Random();
            var minRectSize = 5;
            var maxRectSize = 100;

            for (int i = 0; i < 100; i++)
            {
                var size = new Size(rnd.Next(minRectSize, maxRectSize), rnd.Next(minRectSize, maxRectSize));
                var rect = Layouter.PutNextRectangle(size);
                rectangles.Add(rect);
            }

            foreach (var rect1 in rectangles)
                foreach (var rect2 in rectangles)
                    if (rect1 != rect2)
                        rect1.IntersectsWith(rect2).Should().BeFalse();
        }

    }
}
