using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace TagsCloud.Contracts;

public interface IOutputProcessorOptions
{
    IImageEncoder ImageEncoder { get; set; }
    Size ImageSize { get; set; }
    Color BackgroundColor { get; set; }
}