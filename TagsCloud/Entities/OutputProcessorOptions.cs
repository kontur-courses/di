using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using TagsCloud.Contracts;

namespace TagsCloud.Entities;

public class OutputProcessorOptions : IOutputProcessorOptions
{
    public IImageEncoder ImageEncoder { get; set; }
    public Size ImageSize { get; set; }
    public Color BackgroundColor { get; set; }
}