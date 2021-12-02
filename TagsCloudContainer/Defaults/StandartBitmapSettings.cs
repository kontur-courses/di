using System.Drawing;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class StandartBitmapSettings : IBitmapSettingsProvider
{
    public void Apply(Graphics graphics)
    {
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    }

    public Bitmap CreateEmptyBitmap()
    {
        return new Bitmap(800, 400);
    }
}