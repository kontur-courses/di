using FluentAssertions;
using NUnit.Framework;
using SixLabors.ImageSharp;
using TagsCloudVisualization;

namespace TagsCloud.Tests;

[TestFixture]
public class PointFExtensionsTests
{
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        random = new Random();
    }

    private Random random;

    [Test]
    public void Center_Should_CenterPoint()
    {
        for (var i = 0; i < 1000; i++)
        {
            var center = new PointF(random.Next(1, 2500), random.Next(1, 2500));
            var actual = new PointF(random.Next(1, 2500), random.Next(1, 2500));
            var expected = new PointF(actual.X + center.X, actual.Y + center.Y);

            actual.Center(center).Should().Be(expected);
        }
    }

    [Test]
    public void ApplyOffset_Should_ApplyOffset()
    {
        for (var i = 0; i < 1000; i++)
        {
            var (offsetX, offsetY) = (random.Next(1, 2500), random.Next(1, 2500));
            var actual = new PointF(random.Next(1, 2500), random.Next(1, 2500));
            var expected = new PointF(actual.X + offsetX, actual.Y + offsetY);

            actual.ApplyOffset(offsetX, offsetY).Should().Be(expected);
        }
    }

    [Test]
    public void ConvertToCartesian_Should_ReturnCorrectValues()
    {
        for (var i = 0; i < 1000; i++)
        {
            var actual = new PointF(random.Next(1, 2500), random.Next(1, 2500));
            var expected = new PointF(
                actual.X * (float)Math.Cos(actual.Y),
                actual.X * (float)Math.Sin(actual.Y));

            actual.ConvertToCartesian().Should().Be(expected);
        }
    }
}