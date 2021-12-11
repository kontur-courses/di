using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Visualizers
{
    public class RandomTagColoring : ITagColoring
    {
        private readonly Color[] colors;

        public RandomTagColoring(IEnumerable<Color> colors)
        {
            this.colors = colors.ToArray();
        }

        public Color GetNextColor()
        {
            var random = new Random();
            return colors[random.Next(colors.Length)];
        }
    }
}
