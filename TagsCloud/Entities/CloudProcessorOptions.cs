using SixLabors.Fonts;
using SixLabors.ImageSharp;
using TagsCloud.Contracts;
using TagsCloudVisualization;

namespace TagsCloud.Entities;

public class CloudProcessorOptions : ICloudProcessorOptions
{
    public ColoringStrategy ColoringStrategy { get; init; }
    public Color[] Colors { get; init; }
    public FontFamily FontFamily { get; init; }
    public ILayout Layout { get; init; }
    public MeasurerType MeasurerType { get; init; }
    public SortType Sort { get; init; }
    public int MaxFontSize { get; init; }
    public int MinFontSize { get; init; }
}