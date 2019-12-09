using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudGenerator.Visualizer
{
    public class CyclicColoringAlgorithm : IColoringAlgorithm
    {
        public IEnumerable<Color> GetColors(ImageSettings imageSettings)
        {
            var colors = imageSettings.Colors.ToList();
            var colorsCount = colors.Count;
            var shift = 0;

            while (true)
            {
                yield return colors[shift];
                shift = (shift + 1) % colorsCount;
            }
        }
    }
}