using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization;

namespace TagsCloudContainer.Painting
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
            foreach (var (color, background) in GetNextColor(colors).Zip(GetNextColor(backgroundColors)))
                yield return new Palette(color, background);
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