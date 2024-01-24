using SixLabors.Fonts;
using TagsCloud.Colorizers;

namespace TagsCloud.Entities;

public record CloudTagFactoryOptions(
    FontFamily FontFamily,
    ColorizerBase Colorizer,
    LayoutOptions LayoutOptions);