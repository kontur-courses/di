using System.Drawing;

namespace TagCloudTest.CloudLayouter.SpiralCloudLayouters.Infrastructure;

public class CloudDrawingSettings
{
    public readonly Size Size;
    public readonly Point Center;
    public readonly Color BgColor;
    public readonly Color RectangleColor;
    public readonly bool DrawCenter;
    public readonly bool DrawCircle;

    public CloudDrawingSettings(
        Size size, Point center,
        Color bgColor, Color rectangleColor,
        bool drawCenter = false, bool drawCircle = false)
    {
        Size = size;
        Center = center;
        BgColor = bgColor;
        RectangleColor = rectangleColor;
        DrawCenter = drawCenter;
        DrawCircle = drawCircle;
    }
}