using System.Drawing;

namespace TagsCloudVisualization.Abstractions;

public interface IBitmapSettingsProvider
{
    void Apply(Graphics graphics);

    Bitmap CreateEmptyBitmap();
}