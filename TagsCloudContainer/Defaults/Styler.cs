using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class Styler : IStyler
{
    private readonly StyleProvider settings;

    public Styler(StyleProvider settings)
    {
        this.settings = settings;
    }

    public IStyledTag Style(ITag source)
    {
        var style = settings.GetStyle(source);
        return new StyledTag(source.Value, style);
    }
}
