using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using System.Drawing;

namespace TagsCloudVisualization.Tests
{
    class RectangleExtension_should
    {
        private static Point point;

        [SetUp]
        public void SetUp()
        {
            point = new Point(4, 4);
        }

        [TestCase(0, 0, 4, 4, TestName = "GetCenter_WhenRectangleIsPoint_ReturnThisPoint")]
        [TestCase(10, 10, 9, 9, TestName = "GetCenter_WhenRectangleWithEvenSize_ReturnCorrectCenter")]
        [TestCase(-10, -10, -1, -1, TestName = "GetCenter_WhenRectangleWithEvenNegativeSize_ReturnCenterAsIfRectangleIsInverted")]
        [TestCase(-11, -11, -1, -1, TestName = "GetCenter_WhenRectangleWithOddNegativeSize_ReturnCenterAsIfRectangleIsInverted")]
        [TestCase(-11, 11, -1, 9, TestName = "GetCenter_WhenRectangleWithNegativeWidth_ReturnCenterAsIfRectangleIsInverted")]
        [TestCase(11, -11, 9, -1, TestName = "GetCenter_WhenRectangleWithNegativeHeight_ReturnCenterAsIfRectangleIsInverted")]
        [TestCase(11, 11, 9, 9, TestName = "GetCenter_WhenRectangleWithOddSize_ReturnCenterWithRoundingDownCoordinates")]
        [TestCase(11, 10, 9, 9, TestName = "GetCenter_WhenRectangleWithOddWidth_ReturnCenterWithRoundingDownCoordinates")]
        [TestCase(10, 11, 9, 9, TestName = "GetCenter_WhenRectangleWithOddHeight_ReturnCenterWithRoundingDownCoordinates")]
        [TestCase(0, 10, 4, 9, TestName = "GetCenter_WhenRectangleIsVerticalLine_ReturnPointOnThisLine")]
        [TestCase(10, 0, 9, 4, TestName = "GetCenter_WhenRectangleIsHorizontalLine_ReturnPointOnThisLine")]
        public void GetCenter_WhenRectangleIsLine_ReturnPointOnThisLine(int actualWidth, int actualHeight, int centerExpectedX, int centerExpectedY)
        {
            var rectangle = new Rectangle(point, new Size(actualWidth, actualHeight));
            rectangle.GetCenter().Should().Be(new Point(centerExpectedX, centerExpectedY));
        }

        [TestCase(0, 0, 50, 50, -50, -50, 50, -50, -50, 50, ExpectedResult = false, TestName = "IntersectsWith_WhenRectanglesIntersectsOnPoint_ReturnFalse")]
        [TestCase(0, 0, 0, -50, -50, 0, 50, 0, 0, 50, ExpectedResult = false, TestName = "IntersectsWith_WhenRectanglesIntersectsOnEdge_ReturnFalse")]
        [TestCase(0, 0, 0, 0, ExpectedResult = true, TestName = "IntersectsWith_WhenRectangleEqualOneRectangle_ReturnTrue")]
        [TestCase(-150, -150, 150, 150, ExpectedResult = false, TestName = "IntersectsWith_WhenRectangleWithCoordinatesWithOppositeSign_ReturnFalse")]
        [TestCase(0, 0, 100, 100, 200, 200, ExpectedResult = false, TestName = "IntersectsWith_WhenRectanglesDontIntersects_ReturnFalse")]
        public bool IntersectsWithOtherRectangles(int actualX, int actualY, params int[] args)
        {
            var rectangle = new Rectangle(actualX, actualY, 50, 50);
            var rectangles = new List<Rectangle>();
            for (int i = 0; i < args.Length / 2; i++)
            {
                rectangles.Add(new Rectangle(args[i], args[i + 1], 50, 50));
            }
            return rectangle.IntersectsWith(rectangles);
        }

        [TestCase(0, 0, 100, 100, 0, 0, 10, 10, TestName = "IntersectsWith_WhenRectangleContainsRectangle_ReturnTrue")]
        [TestCase(10, 10, 10, 10, 0, 0, 100, 100, TestName = "IntersectsWith_WhenRectangleComesInRectangle_ReturnTrue")]
        public void IntersectsWithContainsRectangles_ShouldTrue(int firstX, int firstY, int firstWidth, int firstHeight, int secondX, int secondY, int secondWidth, int secondHeight)
        {
            var rectangle = new Rectangle(firstX, firstY, firstWidth, firstHeight);
            var rectangles = new List<Rectangle>(){new Rectangle(secondX,secondY,secondWidth,secondHeight)};
            rectangle.IntersectsWith(rectangles).Should().BeTrue();
        }
    }
}
