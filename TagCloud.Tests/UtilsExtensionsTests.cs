using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using TagCloud.Utils.Extensions;

namespace TagCloud.Tests;

public class UtilsExtensionsTests
{
    [Test]
    public void TryConvertToImageFormat_ReturnsFalseOnIncorrectFormat()
    {
        "abcd"
            .TryConvertToImageFormat(out _)
            .Should()
            .BeFalse();
    } 
    
    [Test]
    public void TryConvertToImageFormat_ParsesCorrectly()
    {
        ImageFormat format;
        
        "png"
            .TryConvertToImageFormat(out format)
            .Should()
            .BeTrue();

        format
            .Should()
            .Be(ImageFormat.Png);
    }

    [Test]
    public void TryParseColor_ReturnsFalseOnIncorrectScheme()
    {
        (300, 300, 300)
            .TryParseColor(out _)
            .Should()
            .BeFalse();
    }
    
    [Test]
    public void TryParseColor_ParsesColorCorrectly()
    {
        Color color;
        
        (000, 111, 222)
            .TryParseColor(out color)
            .Should()
            .BeTrue();

        color
            .Should()
            .Be(Color.FromArgb(255, 0, 111, 222));
    }
}