using System.Drawing;

namespace TagsCloudContainer
{
    public interface ICloudColorPainter
    {
        Color GetRectangleColor(Point cloudCenter, Rectangle currentRectangle, int cloudRadius);
    }
}