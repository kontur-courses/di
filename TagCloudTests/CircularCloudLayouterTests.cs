using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Castle.Windsor;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud;

namespace TagCloudTests
{
    [TestFixture]
    public class CircularCloudLayouterTests
    {
        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) return;
            var rectangles = TestContext.CurrentContext.Test.Arguments.FirstOrDefault() as List<RectangleF>;
            var path = Path.GetTempPath();
            SaveCloudWhenTestIsFallAndGetName(rectangles, TestContext.CurrentContext, path);
            Console.WriteLine("Tag cloud visualization saved in directory:");
            Console.WriteLine($"{path}{TestContext.CurrentContext.Test.Name}.png");
        }

        public static WindsorContainer Container { get; set; } = TagCloud.Program.GetContainer();

        public static class LayouterSource
        {
            private static readonly List<RectangleF> SameSizeRectangles =
                GetSameSizeRectangles();

            private static readonly List<RectangleF> DifferentSizeRectangles =
                GetDifferentSizeRectangles();

            private static readonly TestCaseData DifferentSizeData =
                new TestCaseData(DifferentSizeRectangles, new Point(100, 100)).SetName("DifferentSizeRectanglesTest");

            private static readonly TestCaseData SameSizeData =
                new TestCaseData(SameSizeRectangles, new Point(100, 100)).SetName("SameSizeRectanglesTest");

            private static readonly TestCaseData[] TestCases = {DifferentSizeData, SameSizeData};

            public static List<RectangleF> GetSameSizeRectangles()
            {
                var layouter = Container.Resolve<ICircularCloudLayouter>();
                var sameSizeRectangles = new List<RectangleF>();
                var center = new Point(100, 100);
                for (var i = 0; i < 50; i++)
                    sameSizeRectangles.Add(layouter.PutNextRectangle(new SizeF(5, 5), center));
                return sameSizeRectangles;
            }

            public static List<RectangleF> GetDifferentSizeRectangles()
            {
                var layouter = Container.Resolve<ICircularCloudLayouter>();
                var differentSizeRectangles = new List<RectangleF>();
                var center = new Point(100, 100);
                var random = new Random();
                for (var i = 0; i < 50; i++)
                    differentSizeRectangles.Add(
                        layouter.PutNextRectangle(new SizeF(random.Next(10, 20), random.Next(10, 20)), center));
                return differentSizeRectangles;
            }
        }

        private static double GetSegment(Point start, Point end)
        {
            return Math.Sqrt((start.X - end.X) * (start.X - end.X) + (start.Y - end.Y) * (start.Y - end.Y));
        }

        private void SaveCloudWhenTestIsFallAndGetName(List<RectangleF> rectangles
            , TestContext context, string path)
        {
            var image = new Bitmap(200, 200);
            using (var graphics = Graphics.FromImage(image))
            {
                foreach (var rectangle in rectangles)
                    graphics.FillRectangle(new SolidBrush(Color.Black), rectangle);
            }

            var name = $"{context.Test.Name}.png";
            image.Save(Path.Combine(path, $"{name}"));
        }

        [Test]
        [TestCaseSource(typeof(LayouterSource), "TestCases")]
        public void Cloud_Should_BeDenseAndRound(List<RectangleF> rectangles, Point center)
        {
            var maxRadius = rectangles
                .Max(rec => GetSegment(new Point((int) rec.X + (int) rec.Width / 2, (int) rec.Y + (int) rec.Height / 2),
                    center));
            var area = (double) rectangles.Select(rec => rec.Height * rec.Width).Sum();
            (area / (maxRadius * maxRadius * Math.PI)).Should().BeGreaterOrEqualTo(0.6);
        }


        [Test]
        [TestCaseSource(typeof(LayouterSource), "TestCases")]
        public void Rectangles_ShouldNot_Intersect(List<RectangleF> rectangles, Point center)
        {
            rectangles
                .Any(first =>
                    rectangles.Any(second => second != first && first.IntersectsWith(second))).Should().BeFalse();
        }
    }
}