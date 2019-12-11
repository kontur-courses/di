using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudGenerator.Visualizer
{
    public class ColoringRandomColorAlgorithm : IColoringAlgorithm
    {
        public IEnumerable<Color> GetColors(ImageSettings imageSettings)
        {
            var colors = imageSettings.Colors;
            var index = new Random().Next(colors.Count);

            while (true)
            {
                yield return colors[index];
            }
        }
    }
}