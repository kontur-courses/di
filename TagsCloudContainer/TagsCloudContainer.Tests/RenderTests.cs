using Codeuctivity;
using FluentAssertions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using TagsCloudContainer.Render.CircularCloud;
using Xunit;

namespace TagsCloudContainer.Tests;

public class RenderTests
{
    [Fact]
    public void RenderAndCompare()
    {
        var options = new CircularCloudRenderOptions
        {
            TextColors = new[] {Color.Aqua},
            MinimumFontSize = 96
        };
        var render = new CircularCloudRender(options, new CircularCloudLayouter(options, new LogarithmSpiral(options)));
        (string Word, int Count)[] words = {("foo", 100), ("bar", 200), ("baz", 300)};
        var result = render.Render(words);
        result.Height.Should().Be(options.ImageHeight);
        result.Width.Should().Be(options.ImageWidth);
        var expected = Image.Load<Rgba32>("Data/test.png");
        ImageSharpCompare.ImagesAreEqual((Image<Rgba32>) result, expected).Should().BeTrue();
    }
}