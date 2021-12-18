using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public class BlackWhiteScheme : IColorScheme
    {
        private readonly List<Brush> colors;
        private int colorNumber;

        public BlackWhiteScheme()
        {
            colors = new List<Brush>
            {
                Brushes.White,
                Brushes.Black
            };
            colorNumber = -1;
        }

        public Brush GetNextColor()
        {
            colorNumber++;
            colorNumber %= colors.Count;
            return colors[colorNumber];
        }

        public void Dispose()
        {
            foreach (var color in colors)
                color.Dispose();
        }
    }
}
