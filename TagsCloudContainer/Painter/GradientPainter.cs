using System.Drawing;

namespace TagsCloudContainer
{
    public class GradientPainter : ICloudColorPainter
    {
        public Color GetRectangleColor(Point cloudCenter, Rectangle currentRectangle, int cloudRadius)
        {
            var distanceToCenter = currentRectangle.Location.DistanceTo(cloudCenter);
            var red = (int)(distanceToCenter / cloudRadius * 255 * 0.8);
            var green = (int)(distanceToCenter / cloudRadius * 255 * 0.4);
            var blue = (int)(distanceToCenter / cloudRadius * 255 * 0.9);
            return Color.FromArgb(red, green, blue);
        }
    }
}