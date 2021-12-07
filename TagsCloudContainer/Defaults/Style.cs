using System.Drawing;

namespace TagsCloudContainer.Defaults;

public class Style : IDisposable
{
    private bool disposedValue;

    public Font Font { get; }
    public Brush Brush { get; }

    public Style(Font font, Brush brush)
    {
        Font = font;
        Brush = brush;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            Font.Dispose();
            Brush.Dispose();
            disposedValue = true;
        }
    }

    ~Style()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}