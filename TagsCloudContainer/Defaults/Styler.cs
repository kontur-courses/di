using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Styler : IStyler, IDisposable
    
{
    private readonly DefaultStyle settings;

    public Styler(DefaultStyle settings)
    {
        this.settings = settings;
    }

    public void Dispose()
    {
        settings.Dispose();
        GC.SuppressFinalize(this);
    }

    public IStyledTag Style(ITag source)
    {
        var (font, brush) = settings.GetStyle(source);
        return new StyledTag(source.Value, font, brush);
    }
}
