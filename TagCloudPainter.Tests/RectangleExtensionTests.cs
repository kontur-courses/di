using System;
using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloudPainter.Extensions;

namespace TagCloudPainter.Tests;

public class RectangleExtensionTests
{
    [TestCase(0, 3, 2, 2, TestName = "{m}_ReturnTrue_When_Rectangle_Intersects_Other_Rectangles",
        ExpectedResult = true)]
    [TestCase(-2, 0, 2, 2, TestName = "{m}_ReturnFalse_When_Rectangle_Intersects_Other_Rectangles",
        ExpectedResult = false)]
    public bool IsIntersectsOthersRectangles_Should(int x, int y, int width, int height)
    {
        var rectangle = new Rectangle(x, y, width, height);
        var notIntersectRectangles = new List<Rectangle>
        {
            new(-1, 4, 2, 2),
            new(1, 1, 2, 2)
        };

        var result = rectangle.IsIntersectsOthersRectangles(notIntersectRectangles);

        return result;
    }

    [Test]
    public void IsIntersectsOthersRectangles_Should_Return_False_On_EmptyEnumerable()
    {
        var rectangle = new Rectangle(0, 0, 1, 1);
        var rectangles = new List<Rectangle>();

        var result = rectangle.IsIntersectsOthersRectangles(rectangles);

        result.Should().BeFalse();
    }

    [Test]
    public void IsIntersectsOthersRectangles_Should_Return_False_On_NullEnumerable()
    {
        var rectangle = new Rectangle(0, 0, 1, 1);

        Action action = () => rectangle.IsIntersectsOthersRectangles(null);

        action.Should().Throw<ArgumentNullException>();
    }
}