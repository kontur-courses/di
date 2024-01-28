using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace TagsCloud.Contracts;

public interface IOutputProcessorOptions
{
    IImageEncoder ImageEncoder { get; }
    Size ImageSize { get; }
    Color BackgroundColor { get; }
}