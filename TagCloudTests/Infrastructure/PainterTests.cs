using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloud.App.UI.Console.Common;
using TagCloud.Infrastructure.Layouter;
using TagCloud.Infrastructure.Painter;

namespace TagCloudTests.Infrastructure;

internal class PainterTests
{
    private CloudLayouterFactory layouterFactory;
    private IPalette palette;
    private AppSettings settings;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        settings = new AppSettings { ImageWidth = 1000, ImageHeight = 2000 };
        var layouter = new CircularCloudLayouter(settings);
        layouterFactory = new CloudLayouterFactory(new ICloudLayouter[] {layouter}, layouter);
        palette = new RandomPalette();
    }

    [Test]
    public void Constructor_ThrowArgumentException_WhenImageSizesIsInvalid()
    {
        var invalidSettings = new AppSettings { ImageHeight = -1, ImageWidth = -1 };

        var action = () => new Painter(palette, layouterFactory, invalidSettings);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Image sizes must be great than zero, but was -1x-1");
    }

    [Test]
    public void CreateImage_ShouldCreateImageWithSizeFromSettings()
    {
        var painter = new Painter(palette, layouterFactory, settings);

        var bitmap = painter.CreateImage(new Dictionary<string, int> { { "test", 1 } });

        bitmap.Size.Width.Should().Be(settings.ImageWidth);
        bitmap.Size.Height.Should().Be(settings.ImageHeight);
    }

    [Test]
    public void CreateImage_ShouldThrowArgumentException_WhenInputIsEmptyCollection()
    {
        var painter = new Painter(palette, layouterFactory, settings);

        var action = () => painter.CreateImage(new Dictionary<string, int>());

        action.Should().Throw<InvalidOperationException>().WithMessage("Impossible to save an empty tag cloud");
    }
}