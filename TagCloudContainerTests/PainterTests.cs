﻿using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagCloudContainer.Infrastructure.Common;
using TagCloudContainer.Infrastructure.Layouter;
using TagCloudContainer.Infrastructure.Painter;
using TagCloudContainer.Infrastructure.WordWeigher;

namespace TagCloudContainerTests;

internal class PainterTests
{
    private CircularCloudLayouter layouter;
    private IPalette palette;
    private AppSettings settings;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        settings = new AppSettings() {ImageWidth = 1000, ImageHeight = 2000};
        layouter = new CircularCloudLayouter(settings);
        palette = new RandomPalette();
    }

    [Test]
    public void Constructor_ThrowArgumentException_WhenImageSizesIsInvalid()
    {
        var invalidSettings = new AppSettings { ImageHeight = -1, ImageWidth = -1 };

        var action = () => new Painter(palette, layouter, invalidSettings);

        action.Should().Throw<ArgumentException>()
            .WithMessage("Image sizes must be great than zero, but was -1x-1");
    }

    [Test]
    public void CreateImage_ShouldCreateImageWithSizeFromSettings()
    {
        var painter = new Painter(palette, layouter, settings);

        var bitmap = painter.CreateImage(new List<WeightedWord> { new("test", 1) });

        bitmap.Size.Width.Should().Be(settings.ImageWidth);
        bitmap.Size.Height.Should().Be(settings.ImageHeight);
    }

    [Test]
    public void CreateImage_ShouldThrowArgumentException_WhenInputIsEmptyCollection()
    {
        var painter = new Painter(palette, layouter, settings);

        var action = () => painter.CreateImage(new List<WeightedWord>());

        action.Should().Throw<InvalidOperationException>().WithMessage("Impossible to save an empty tag cloud");
    }
}