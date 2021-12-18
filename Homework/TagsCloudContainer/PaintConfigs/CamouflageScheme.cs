using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public class CamouflageScheme : IColorScheme
    {
        private readonly List<Brush> colors;

        public CamouflageScheme()
        {
            colors = new List<Brush>
            {
                Brushes.DarkOliveGreen,
                Brushes.DarkGreen,
                Brushes.DarkSlateGray,
                Brushes.OliveDrab,
                Brushes.ForestGreen
            };
        }

        public Brush GetNextColor()
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            return colors[rnd.Next(0, colors.Count)];
        }

        public void Dispose()
        {
            foreach (var color in colors)
                color.Dispose();
        }
    }
}
