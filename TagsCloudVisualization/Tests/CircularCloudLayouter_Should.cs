using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagsCloudVisualization.Infrastructure;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    public class CircularCloudLayouter_Should
    {
        private const string LayoutTestCategory = "layout test";
        private CircularCloudLayouter defaultLayouter;
        private Point defaultCenter;
        private Size defaultSize;

        [SetUp]
        public void SetUp()
        {
            defaultCenter = Point.Empty;
            defaultSize = new Size(1000, 1000);
            defaultLayouter = new CircularCloudLayouter(defaultCenter, defaultSize);
        }

        [TestCase(0, 0, TestName = "zero size")]
        [TestCase(-1, 0, TestName = "negative width")]
        [TestCase(-100, -100, TestName = "negative width and height")]
        public void Ctor_ShouldThrow_OnIncorrectPictureSize(int width, int height)
        {
            Action create = () => new CircularCloudLayouter(Point.Empty, new Size(width, height));

            create.ShouldThrowExactly<ArgumentException>();
        }


        [TestCaseSource(nameof(PutNextRectangleShouldReturnInCenterTestCases))]
        public Point PutNextRectangle_ShouldReturnInCenter_WhenPutOnlyOne(Point center, Size size)
        {
            var layouter = new CircularCloudLayouter(center, new Size(1000, 1000));

            var rectangle = layouter.PutNextRectangle(size);

            return rectangle.Location;
        }

        public static IEnumerable PutNextRectangleShouldReturnInCenterTestCases
        {
            get
            {
                yield return new TestCaseData(Point.Empty, new Size(2, 2)).Returns(new Point(-1, -1))
                    .SetName("when center is default");

                yield return new TestCaseData(new Point(1, 1), new Size(1, 1)).Returns(new Point(1, 1))
                    .SetName("when center is shifted");
            }
        }

        [TestCaseSource(nameof(PutNextRectangleShouldThrowTestCases))]
        public void PutNextRectangle_ShouldThrow_OnIncorrectSize(Size size)
        {
            Action put = () => defaultLayouter.PutNextRectangle(size);

            put.ShouldThrowExactly<ArgumentException>();
        }

        public static IEnumerable PutNextRectangleShouldThrowTestCases
        {
            get
            {
                yield return new TestCaseData(Size.Empty).SetName("empty size");

                yield return new TestCaseData(new Size(-1, -1)).SetName("negative width and height");

                yield return new TestCaseData(new Size(-1, 0)).SetName("negative width and zero height");
            }
        }

        [TestCaseSource(nameof(PutNextRectangleShouldReturnCommonTestCases))]
        public void PutNextRectangle_ShouldReturnDisjoint(Size[] sizes)
        {
            var rectangles = PutMultipleRectangles(defaultLayouter, sizes);

            foreach (var rectangle in rectangles)
            {
                rectangles.Count(r => r == rectangle).Should().Be(1, $"rectangles should be unique");
                rectangles.Where(r => r != rectangle).Any(r => r.IntersectsWith(rectangle)).Should()
                    .BeFalse("rectangles should not intersect");
            }
        }

        [TestCaseSource(nameof(PutNextRectangleShouldThrowWhenRectangleCannotBePlacedTestCases))]
        public void PutNextRectangle_ShouldThrow_WhenRectangleCannotBePlaced(Size[] sizes)
        {
            Action put = () => PutMultipleRectangles(defaultLayouter, sizes);

            put.ShouldThrowExactly<ArgumentException>();
        }

        public static IEnumerable PutNextRectangleShouldThrowWhenRectangleCannotBePlacedTestCases
        {
            get
            {
                yield return new TestCaseData(new[] {new Size(2000, 2000)}).SetName("one big rectangle");

                yield return new TestCaseData(new[]
                        {new Size(500, 500), new Size(600, 600), new Size(500, 500), new Size(500, 500),})
                    .SetName("some big rectangles");

                yield return new TestCaseData(Enumerable.Repeat(new Size(50, 50), 600).ToArray()).SetName(
                    "many little rectangles");
            }
        }

        [TestCaseSource(nameof(PutNextRectangleShouldReturnCommonTestCases))]
        public void PutNextRectangle_ShouldReturnNearlyCircleShaped(Size[] sizes)
        {
            var rectangles = PutMultipleRectangles(defaultLayouter, sizes);

            var maxDiagonal = rectangles.Max(r =>
                GeometryHelper.GetDistanceBetweenPoints(r.Location, new Point(r.Right, r.Bottom)));
            ConvexHull.MakeHull(rectangles.SelectMany(GeometryHelper.GetRectangleNodes).ToList())
                .Select(p => GeometryHelper.GetDistanceBetweenPoints(p, defaultCenter))
                .CartesianSquare()
                .Select(p => Math.Abs(p.Item2 - p.Item1))
                .Should()
                .OnlyContain(x => x <= maxDiagonal);
        }

        [TestCaseSource(nameof(PutNextRectangleShouldReturnCommonTestCases))]
        public void PutNextRectangle_ShouldReturnCloseToEachOther(Size[] sizes)
        {
            var rectangles = PutMultipleRectangles(defaultLayouter, sizes);

            var maxDiagonal = rectangles.Max(r =>
                GeometryHelper.GetDistanceBetweenPoints(r.Location, new Point(r.Right, r.Bottom)));
            foreach (var rectangle in rectangles)
            {
                rectangles.Where(r => r != rectangle)
                    .Min(r => GeometryHelper.GetDistanceBetweenRectanglesCenters(r, rectangle))
                    .Should()
                    .BeLessOrEqualTo(maxDiagonal);
            }
        }

        public static IEnumerable PutNextRectangleShouldReturnCommonTestCases
        {
            get
            {
                yield return new TestCaseData(new[] {new Size(100, 100), new Size(250, 250), new Size(125, 125)})
                    .SetName("when put some big rectangles").SetCategory(LayoutTestCategory);

                yield return new TestCaseData(Enumerable.Range(1, 10).CartesianSquare()
                    .Select(p => new Size(p.Item1, p.Item2)).ToArray()).SetName(
                    "when put many different little rectangles").SetCategory(LayoutTestCategory);

                yield return new TestCaseData(Enumerable.Range(30, 80).Where(x => x % 10 == 0).CartesianSquare()
                    .Select(x => new Size(x.Item1, x.Item2)).ToArray()).SetName("when many different big rectangles")
                    .SetCategory(LayoutTestCategory);

                yield return new TestCaseData(new[]
                    {
                        new Size(2, 2), new Size(3, 2), new Size(2, 4),
                        new Size(2, 2), new Size(2, 4), new Size(4, 2),
                    })
                    .SetName("when some different little rectangles");

                yield return new TestCaseData(Enumerable.Repeat(new Size(4, 4), 50).ToArray()).SetName(
                    "when many little squares");
            }
        }

        private Rectangle[] PutMultipleRectangles(CircularCloudLayouter layouter, params Size[] sizes)
        {
            return layouter.PutMultipleRectangles(sizes).ToArray();
        }

        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var categories = TestContext.CurrentContext.Test.Properties["Category"];
            if ((status == TestStatus.Failed || status == TestStatus.Inconclusive) && categories.Contains(LayoutTestCategory))
            {
                var sizes = TestContext.CurrentContext.Test.Arguments.FirstOrDefault(a => a is Size[]) as Size[];
                var bitmap = BitmapGenerator.CreateBitmap(defaultCenter, defaultSize, sizes);
                var path = Path.Combine(TestContext.CurrentContext.TestDirectory,
                    $"{TestContext.CurrentContext.Test.MethodName} {TestContext.CurrentContext.Test.Name}");
                bitmap.Save($"{path}.png", ImageFormat.Png);
                TestContext.Out.Write($"Tag cloud visualization saved to file {path}.png");
            }
        }

    }
}