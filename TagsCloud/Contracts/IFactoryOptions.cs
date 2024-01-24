using SixLabors.Fonts;
using TagsCloud.Colorizers;
using TagsCloudVisualization;

namespace TagsCloud.Contracts;

public interface IFactoryOptions
{
    public FontFamily FontFamily { get; set; }
    public ColorizerBase Colorizer { get; set; }
    public ILayout Layout { get; set; }
}