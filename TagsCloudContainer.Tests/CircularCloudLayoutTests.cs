using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudContainer.ResultRenderer;
using TagsCloudContainer.WordLayouts;

namespace TagsCloudContainer.Tests
{
    public class CircularCloudLayoutTests
    {
        private readonly PointF startPoint = PointF.Empty;
        private CircularCloudLayouter layouter;
        private IPosition word;
        private List<RectangleF> rectangles;

        [SetUp]
        public void DoBeforeAnyTest()
        {
            layouter = new CircularCloudLayouter(new Config{CenterPoint = startPoint});
            word = new CustomWord();
            rectangles = new List<RectangleF>();
        }

        [TearDown]
        public void DoAfterAnyTest()
        {
            if (TestContext.CurrentContext.Result.FailCount != 0)
            {
                var savePath = TestContext.CurrentContext.TestDirectory
                    + $"\\test_failed_{TestContext.CurrentContext.Test.Name}.bmp";
                var words = rectangles.Select(r =>
                {
                    var result =
                        new Word(new Font(FontFamily.GenericMonospace, 12), Color.CadetBlue, "test")
                            {Position = r};

                    return result;
                });
                var config = new Config {ImageSize = new Size(2000, 2000)};
                var imageRenderer = new ImageRenderer(config) {DrawRectangles = true};
                imageRenderer.Generate(words)
                    .Save(savePath, ImageFormat.Png);
            }
        }

        [Test]
        public void PutNextRectangle_ReturnsSameSizeRectangleAtStartPoint_OnFirstRectangle()
        {
            var size = new Size(100, 100);
            var expected = word;
            expected.Position = new RectangleF(PointF.Empty, size);

            var result = layouter.GetNextPosition(size);

            result
                .Equals(expected.Position)
                .Should()
                .BeTrue();
        }

        [Test]
        public void PutNextRectangle_ReturnsNotIntersectedRectangle_OnCorrectSizes()
        {
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                var size = new Size(random.Next(1, 100), random.Next(1, 100));
                var generatedRectangle = layouter.GetNextPosition(size);

                foreach (var rectangle in rectangles)
                {
                    generatedRectangle.IntersectsWith(rectangle)
                        .Should()
                        .BeFalse(
                            "generated rectangle {0} mustn't intersect with already added rectangle {1}",
                            generatedRectangle,
                            rectangle);
                }

                rectangles.Add(generatedRectangle);
            }
        }

        #region InvalidSizes

        public static IEnumerable<Size> OnInvalidSizes()
        {
            yield return new Size(0, 0);
            yield return new Size(0, 1);
            yield return new Size(1, 0);
            yield return new Size(-1, 1);
            yield return new Size(1, -1);
            yield return new Size(-1, -1);
        }

        #endregion

        [TestCaseSource(nameof(OnInvalidSizes))]
        public void PutNextRectangle_ThrowsException_OnInvalidSize(Size size)
        {
            Action action = () => layouter.GetNextPosition(size);

            action
                .Should()
                .Throw<ArgumentException>();
        }

        [Test]
        public void PutNextRectangle_GeneratesCircleOfRectangles_OnCorrectSizes()
        {
            GenerateRectangles(200);
            var orderedXCoordinates = GetOrderedUnsignedXCoordinates();
            var orderedYCoordinates = GetOrderedUnsignedYCoordinates();

            var radiuses = new[]
                {
                    orderedXCoordinates.First(),
                    orderedXCoordinates
                        .Skip(1)
                        .First(),
                    orderedXCoordinates
                        .Take(orderedXCoordinates.Length - 1)
                        .Last(),
                    orderedXCoordinates.Last(),
                    orderedYCoordinates.First(),
                    orderedYCoordinates
                        .Skip(1)
                        .First(),
                    orderedYCoordinates
                        .Take(orderedYCoordinates.Length - 1)
                        .Last(),
                    orderedYCoordinates.Last()
                }
                .ToArray();

            foreach (var rad1 in radiuses)
            {
                foreach (var rad2 in radiuses)
                {
                    ((double)rad1 / rad2)
                        .Should()
                        .BeGreaterOrEqualTo(0.7, "radiuses must be similar");
                }
            }
        }

        [Test]
        public void PutNextRectangle_GeneratesTightCloudOfRectangles_OnCorrectSizes()
        {
            GenerateRectangles(200);
            var orderedXCoordinates = GetOrderedUnsignedXCoordinates();
            var orderedYCoordinates = GetOrderedUnsignedYCoordinates();

            var maxRadius = Math.Max(
                Math.Max(orderedXCoordinates.First(), orderedXCoordinates.Last()),
                Math.Max(orderedYCoordinates.First(), orderedYCoordinates.Last()));

            var circleSquare = Math.PI * maxRadius * maxRadius;

            var squareOfRectangles =
                rectangles.Aggregate(
                    0.0,
                    (square, rectangle) => square + rectangle.Width * rectangle.Height);

            squareOfRectangles.Should()
                .BeGreaterOrEqualTo(circleSquare * 0.3);
        }

        private void GenerateRectangles(int amount)
        {
            var random = new Random();

            for (var i = 0; i < amount; i++)
            {
                var size = new Size(random.Next(1, 100), random.Next(1, 100));
                var rectangle = layouter.GetNextPosition(size);
                rectangles.Add(rectangle);
            }
        }

        private float[] GetOrderedUnsignedXCoordinates()
        {
            return rectangles
                .Select(rectangle => rectangle.X > startPoint.X
                    ? rectangle.X + rectangle.Width
                    : rectangle.X)
                .OrderBy(x => x)
                .Select(Math.Abs)
                .Select(x => x - startPoint.X)
                .ToArray();
        }

        private float[] GetOrderedUnsignedYCoordinates()
        {
            return rectangles
                .Select(rectangle => rectangle.Y < startPoint.Y
                    ? rectangle.Y + rectangle.Height
                    : rectangle.Y)
                .OrderBy(y => y)
                .Select(Math.Abs)
                .Select(y => y - startPoint.Y)
                .ToArray();
        }
    }

    public class CustomWord : IPosition
    {
        public CustomWord()
        {
        }

        public RectangleF Position { get; set; }
    }
}