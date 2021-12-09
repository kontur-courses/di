using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization;

namespace TagsCloudContainer
{
    public class PalettesMaker : IPalettesMaker
    {
        public List<Color> colors { get; set; } = new List<Color>
        {
            Color.DarkRed,
            Color.Goldenrod,
            Color.DarkGreen
        };

        public List<Color> backgroundColors { get; set; } = new List<Color>
        {
            Color.YellowGreen,
            Color.DarkGray,
            Color.LightBlue
        };

        public IEnumerable<Palette> GetPalettes()
        {
            var colorEnumerator = GetNextColor(colors).GetEnumerator();
            var backgroundEnumerator = GetNextColor(backgroundColors).GetEnumerator();
            while (true)
            {
                colorEnumerator.MoveNext();
                backgroundEnumerator.MoveNext();
                var color = colorEnumerator.Current;
                var background = backgroundEnumerator.Current;
                yield return new Palette(color, background);
            }
        }

        private IEnumerable<Color> GetNextColor(List<Color> colors)
        {
            var i = 0;
            while (true)
            {
                if (i == colors.Count)
                    i = 0;
                yield return colors[i];
                i++;

            }
        }
    }
}