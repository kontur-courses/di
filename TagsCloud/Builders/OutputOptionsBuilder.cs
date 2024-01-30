using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using TagsCloud.Contracts;
using TagsCloud.Entities;
using TagsCloud.Options;

namespace TagsCloud.Builders;

public class OutputOptionsBuilder
{
    private Color backgroundColor;
    private IImageEncoder imageEncoder;
    private Size imageSize;

    public OutputOptionsBuilder SetImageFormat(ImageFormat format)
    {
        imageEncoder = format switch
        {
            ImageFormat.Jpeg or ImageFormat.Jpg => new JpegEncoder(),
            ImageFormat.Bmp => new BmpEncoder(),
            _ => new PngEncoder()
        };

        return this;
    }

    public OutputOptionsBuilder SetImageSize(Size size)
    {
        imageSize = size;
        return this;
    }

    public OutputOptionsBuilder SetImageBackgroundColor(string hex)
    {
        backgroundColor = Color.TryParseHex(hex, out var color) ? color : Color.White;
        return this;
    }

    public IOutputProcessorOptions BuildOptions()
    {
        return new OutputProcessorOptions
        {
            BackgroundColor = backgroundColor,
            ImageEncoder = imageEncoder,
            ImageSize = imageSize
        };
    }
}