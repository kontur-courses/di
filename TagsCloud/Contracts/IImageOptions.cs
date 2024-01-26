using SixLabors.ImageSharp;

namespace TagsCloud.Contracts;

public interface IImageOptions
{
    Color BackgroundColor { get; set; }
    Size ImageSize { get; set; }
}