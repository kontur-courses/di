using System.Drawing;
using FluentAssertions;
using NUnit.Framework.Interfaces;
using TagsCloud.CloudLayouter.Implementation;
using TagsCloud.Painter;
using TagsCloud.Tests.ImageFromTestSaver.Implementation;

namespace TagsCloud.Tests
{
    [TestFixture]
    public class CircularCloudLayouterShould
    {
        private CircularCloudLayouter circularCloud;
        private List<Rectangle> rectangles;

        private readonly Random random = new();

        [SetUp]
        public void Setup()
        {
            circularCloud = new CircularCloudLayouter(Point.Empty);
            rectangles = new List<Rectangle>();
        }

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed || rectangles.Count == 0) 
                return;

            var painter = new RectanglePainter();
            var bitmapSize = painter.GetBitmapSize(rectangles);
#pragma warning disable CA1416
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
#pragma warning restore CA1416
            var imageSaver = new ErrorHandler();
                
            painter.Paint(rectangles, bitmap, painter.SetRandomRectangleColor);
            var imageSavedSuccessfully =
                imageSaver.TrySaveImageToFile(TestContext.CurrentContext.Test.Name, bitmap, out var path);

            var outputMessage = imageSavedSuccessfully
                    ? $"Tag cloud visualization saved to file <{path}>"
                    : $"Tag cloud visualization not saved";

            TestContext.Out.WriteLine(outputMessage);
        }
        
        [TestCaseSource(typeof(TestData), nameof(TestData.IncorrectSize))]
        [Parallelizable(scope: ParallelScope.All)] 
        public void PutNextRectangle_IncorrectSize_ArgumentException(int width, int height)
        {
            var putNextRectangle = (Action) (() => circularCloud.PutNextRectangle(new Size(width, height)));
            putNextRectangle.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(typeof(TestData), nameof(TestData.CorrectSizes))]
        [Parallelizable(scope: ParallelScope.None)] 
        public void PutNextRectangle_CorrectSize_FigureCount(int[] widths, int[] heights)
        {
            for (var index = 0; index < Math.Min(widths.Length, heights.Length); index++)
                circularCloud.PutNextRectangle(new Size(widths[index], heights[index]));

            circularCloud.Figures.Count.Should().Be(Math.Min(widths.Length, heights.Length));
        }

        [Test]
        [Ignore("Incorrect test for checking TearDown and ErrorHandler")]
        public void PutNextRectangle_IncorrectValues_ErrorPaint()
        {
            for (var index = 0; index < 10; index++)
                rectangles.Add(circularCloud.PutNextRectangle(new Size(random.Next(25, 50), random.Next(25, 50))));
            
            circularCloud.Figures.Count.Should().Be(0);
        }
    }
}