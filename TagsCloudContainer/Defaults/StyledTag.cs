using System.Drawing;
using TagsCloudContainer.Abstractions;

namespace TagsCloudContainer.Defaults;

internal class StyledTag : IStyledTag, IDisposable
{
    private readonly string tag;
    private readonly Style style;
    private bool disposedValue;

    public StyledTag(string tag, Style style)
    {
        this.tag = tag;
        this.style = style;
    }

    public void DrawSelf(Graphics graphics, Rectangle position)
    {
        graphics.DrawString(tag, style.Font, style.Brush, position);
    }

    public Size GetTrueGraphicSize(Graphics graphics)
    {
        return Size.Ceiling(graphics.MeasureString(tag, style.Font));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            style.Dispose();
            disposedValue = true;
        }
    }

    ~StyledTag()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}