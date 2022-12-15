using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud;

namespace TagCloudTests;

[TestFixture]
[Parallelizable(ParallelScope.All)]
public class SizeExtensionsTests
{
    [TestCase(10, 10, true, TestName = "{m}PositiveParameters")]
    [TestCase(0, 0, false, TestName = "{m}ZeroParameters")]
    [TestCase(-10, -10, false, TestName = "{m}NegativeParameters")]
    [TestCase(10, 0, false, TestName = "{m}ZeroHeight")]
    [TestCase(10, -10, false, TestName = "{m}NegativeHeight")]
    [TestCase(0, 10, false, TestName = "{m}ZeroWidth")]
    [TestCase(-10, 10, false, TestName = "{m}NegativeWidth")]
    public void IsPositive_ReturnCorrectBoolean_On(int width, int height, bool expected)
    {
        var size = new Size(width, height);

        var result = size.IsPositive();

        result.Should().Be(expected);
    }

    [Test]
    public void Area_ReturnMultiplyOfSizeSides(
        [Values(-10, -5, -1, 0, 1, 5, 10)] int x,
        [Values(-10, -5, -1, 0, 1, 5, 10)] int y)
    {
        var size = new Size(x, y);
        var expected = x * y;

        var result = size.Area();

        result.Should().Be(expected);
    }
}