using System;
using System.Drawing;

namespace TagsCloudContainer.Painter
{
    public class RandomPainter : ICloudColorPainter
    {
        private readonly Random random = new Random(DateTime.Now.Millisecond);

        public Color GetRectangleColor(Point cloudCenter, Rectangle currentRectangle, int cloudRadius)
        {
            var red = random.Next(255);
            var green = random.Next(255);
            var blue = random.Next(255);
            return Color.FromArgb(red, green, blue);
        }
    }
}