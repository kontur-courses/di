using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using TagsCloud.Contracts;

namespace TagsCloud.Entities;

public class OutputProcessorOptions : IOutputProcessorOptions
{
    public IImageEncoder ImageEncoder { get; init; }
    public Size ImageSize { get; init; }
    public Color BackgroundColor { get; init; }
}