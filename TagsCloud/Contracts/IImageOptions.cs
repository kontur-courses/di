using SixLabors.ImageSharp;

namespace TagsCloud.Contracts;

public interface IImageOptions
{
    public Color BackgroundColor { get; set; }
    public Size ImageSize { get; set; }
}