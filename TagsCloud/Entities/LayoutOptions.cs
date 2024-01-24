using SixLabors.ImageSharp;
using TagsCloudVisualization;

namespace TagsCloud.Entities;

public record LayoutOptions(ILayoutFunction Function, PointF Center);