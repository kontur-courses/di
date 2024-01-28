using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;

namespace TagsCloud.Contracts;

public interface IOutputProcessorOptions
{
    Size ImageSize { get; }
    Color BackgroundColor { get; }
    IImageEncoder ImageEncoder { get; }
}