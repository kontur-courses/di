using SixLabors.Fonts;
using TagsCloud.Colorizers;
using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface IFactoryOptions
{
    FontFamily FontFamily { get; set; }
    ColorizerBase Colorizer { get; set; }
    ILayout Layout { get; set; }
}