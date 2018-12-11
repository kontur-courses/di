using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Painter
{
    public class SolidPainter : ICloudColorPainter
    {
        private readonly Palette palette;

        public SolidPainter(Palette palette)
        {
            this.palette = palette;
        }

        public Color GetRectangleColor(Point cloudCenter, Rectangle currentRectangle, int cloudRadius)
        {
            return palette.PrimaryColor;
        }
    }
}