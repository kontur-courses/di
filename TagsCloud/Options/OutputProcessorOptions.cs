using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using TagsCloud.Contracts;

namespace TagsCloud.Options;

public class OutputProcessorOptions : IOutputProcessorOptions
{
    public Size ImageSize { get; init; }
    public Color BackgroundColor { get; init; }
    public IImageEncoder ImageEncoder { get; init; }
}