using Aspose.Drawing;
using TagCloud.Domain.Settings;

namespace TagCloud.Tests;

public class LayoutSettingsTests
{
    [SetUp]
    public void SetUp()
    {
        settings = new LayoutSettings();
    }

    private LayoutSettings settings;
    
    [Test]
    public void Dimensions_ThrowsArgumentExceptionOnLessThanHundredDimensions()
    {
        new Action(() => settings.Dimensions = new Size(90, 90))
            .Should()
            .ThrowExactly<ArgumentException>()
            .Where(e => e.Message.Equals("Размеры должны быть не менне 100x100", StringComparison.OrdinalIgnoreCase));
    }
    
    [Test]
    public void Dimensions_ShouldNotThrowOnCorrectDimensions()
    {
        new Action(() => settings.Dimensions = new Size(100, 100))
            .Should()
            .NotThrow();
    }
}