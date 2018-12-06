using System.Drawing;

namespace TagsCloudContainer.Painter
{
    public interface ICloudColorPainter
    {
        Color GetRectangleColor(Point cloudCenter, Rectangle currentRectangle, int cloudRadius);
    }
}