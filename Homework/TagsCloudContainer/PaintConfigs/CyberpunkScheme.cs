using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.PaintConfigs
{
    public class CyberpunkScheme : IColorScheme
    {
        private readonly List<Brush> colors;

        public CyberpunkScheme()
        {
            colors = new List<Brush>
            {
                Brushes.Yellow,
                Brushes.Purple,
                Brushes.Blue,
                Brushes.Purple,
                Brushes.DeepSkyBlue,
                Brushes.DeepPink
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
