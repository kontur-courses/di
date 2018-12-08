using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Painter
{
    public class GradientPainter : ICloudColorPainter
    {
        private readonly Palette palette;

        public GradientPainter(Palette palette)
        {
            this.palette = palette;
        }

        public Color GetRectangleColor(Point cloudCenter, Rectangle currentRectangle, int cloudRadius)
        {
            var distanceToCenter = currentRectangle.Location.DistanceTo(cloudCenter);
            //var red = (int)(distanceToCenter / cloudRadius * 255 * 0.8);
            //var green = (int)(distanceToCenter / cloudRadius * 255 * 0.4);
            //var blue = (int)(distanceToCenter / cloudRadius * 255 * 0.9);
            var red = (int)(distanceToCenter / cloudRadius * palette.PrimaryColor.R);
            var green = (int)(distanceToCenter / cloudRadius * palette.PrimaryColor.G);
            var blue = (int)(distanceToCenter / cloudRadius * palette.PrimaryColor.B);
            return Color.FromArgb(red, green, blue);
        }
    }
}