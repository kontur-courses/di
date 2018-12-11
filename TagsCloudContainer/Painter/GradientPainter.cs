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
            var red = (int)(255 - distanceToCenter / cloudRadius * (255 - palette.PrimaryColor.R));
            var green = (int)(255 - distanceToCenter / cloudRadius * (255 - palette.PrimaryColor.G));
            var blue = (int)(255 - distanceToCenter / cloudRadius * (255 - palette.PrimaryColor.B));
            return Color.FromArgb(red, green, blue);
        }
    }
}