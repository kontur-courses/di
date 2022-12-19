using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TagCloud.CloudLayouters;
using TagCloud.PointGenerators;

namespace TagCloudTests
{
    public class CircularCloudLayouterTests
    {
        private readonly string failedTestsPictureFolder = "FailedTestsPicture";
        private CircularCloudLayouter cloudLayouter;

        [SetUp]
        public void PrepareCircularCloudLayouter()
        {
            var center = new Point();
            cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
        }

        [TearDown]
        public void SaveExceptionText_WhenTestFailed()
        {
            var context = TestContext.CurrentContext;
            if (context.Result.Outcome.Status != TestStatus.Failed)
                return;

            Directory.CreateDirectory(failedTestsPictureFolder);
            var fileName = $"{context.Test.MethodName}{new Random().Next()}";
            var filePath = Path.Combine(failedTestsPictureFolder, fileName);

            File.WriteAllText(filePath + ".txt", $"The test {context.Test.FullName} failed with an error: {context.Result.Message}" + 
                                                 Environment.NewLine + "StackTrace:" + context.Result.StackTrace);
        }

        [TestCase(0, 0, TestName = "in zero point")]
        [TestCase(5, 10, TestName = "in point(5, 10)")]
        public void CircularCloudLayouter_Ctor_SetCenterPoint(int x, int y)
        {
            var planningCenter = new Point(x, y);

            cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(planningCenter));

            cloudLayouter.Center.Should().BeEquivalentTo(planningCenter);
        }

        [TestCase(0, 0, TestName = "width and height are equal to zero")]
        [TestCase(0, 10, TestName = "width is zero")]
        [TestCase(10, 0, TestName = "height is zero")]
        public void CircularCloudLayouter_PutNextRectangle_ThrowArgumentException_When(int width, int height)
        {
            Action act = () => cloudLayouter.PutNextRectangle(new Size(width, height));

            act.Should().Throw<ArgumentException>();
        }

        [TestCase(0, 0, 350, 750)]
        [TestCase(3, 3, 500, 500)]
        public void CircularCloudLayouter_PutNextRectangle_ReturnedNotIntersectedRectangle(int centerX, int centerY, int firstRectWidth, int firstRectHeight)
        {
            var center = new Point(centerX, centerY);
            cloudLayouter = new CircularCloudLayouter(() => new SpiralPointGenerator(center));
            var rectangles = new List<Rectangle>();
            do
            {
                var rectSize = new Size(firstRectWidth, firstRectHeight);
                var newRect = cloudLayouter.PutNextRectangle(rectSize);

                rectangles.All(rect => rect.IntersectsWith(newRect) == false).Should().BeTrue();
                rectangles.Add(newRect);
                firstRectHeight /= 2;
                firstRectWidth /= 2;
            } while (firstRectHeight > 1 && firstRectWidth > 1);
        }
    }
}