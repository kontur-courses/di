using System.Drawing;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

internal class StyledTag : IStyledTag, IDisposable
{
    private readonly string tag;
    private readonly Font font;
    private readonly Brush brush;
    private bool disposedValue;

    public StyledTag(string tag, Font font, Brush brush)
    {
        this.tag = tag;
        this.font = font;
        this.brush = brush;
    }

    public void DrawSelf(Graphics graphics, Rectangle position)
    {
        graphics.DrawString(tag, font, brush, position);
    }

    public Size GetTrueGraphicSize(Graphics graphics)
    {
        return Size.Ceiling(graphics.MeasureString(tag, font));
    }

    public virtual void Dispose()
    {
        if (!disposedValue)
        {
            font.Dispose();
            brush.Dispose();

            disposedValue = true;
        }

        GC.SuppressFinalize(this);
    }
}