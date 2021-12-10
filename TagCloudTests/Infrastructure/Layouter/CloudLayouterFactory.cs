using System;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.Infrastructure.Layouter;

namespace TagCloudTests.Infrastructure.Layouter;

internal class CloudLayouterFactoryTests
{
    private CloudLayouterFactory sut;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        var circularLayouter = new CircularCloudLayouter(new Point(0, 0));
        var mockLayouter = new FakeCloudLayouter();

        sut = new CloudLayouterFactory(new ICloudLayouter[] { circularLayouter, mockLayouter }, circularLayouter);
    }

    [TestCase("circular", typeof(CircularCloudLayouter))]
    [TestCase("fake", typeof(FakeCloudLayouter))]
    [TestCase("unknown", typeof(CircularCloudLayouter))]
    public void Create_ShouldReturnCorrectLayouter(string layouterName, Type expectedType)
    {
        var actual = sut.Create(layouterName);

        actual.Should().BeOfType(expectedType);
    }

    public class FakeCloudLayouter : ICloudLayouter
    {
        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            return new Rectangle();
        }

        public Rectangle[] GetLayout()
        {
            return Array.Empty<Rectangle>();
        }
    }
}