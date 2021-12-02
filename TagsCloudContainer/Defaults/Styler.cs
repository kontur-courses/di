using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Styler : IStyler
{
    private readonly IStylerSettings settings;

    public Styler(IStylerSettings settings)
    {
        this.settings = settings;
    }

    public IStyledTag Style(ITag source)
    {
        var (font, brush) = settings.GetStyle(source);
        return new StyledTag(source.Value, font, brush);
    }
}
