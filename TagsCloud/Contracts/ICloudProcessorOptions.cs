using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Entities;
using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface ICloudProcessorOptions
{
    ColoringStrategy ColoringStrategy { get; init; }
    Color[] Colors { get; init; }
    FontFamily FontFamily { get; init; }
    ILayout Layout { get; init; }
    int MaxFontSize { get; init; }
    int MinFontSize { get; init; }
}