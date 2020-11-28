using System.Drawing;

namespace WordCloudGenerator
{
    public class Palette : IPalette
    {
        private readonly Color[] colors = {Color.Navy, Color.Red, Color.Gold, Color.Green};
        private int colorIndex = -1;

        public Color GetNextColor()
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            return colors[colorIndex];
        }
    }
}