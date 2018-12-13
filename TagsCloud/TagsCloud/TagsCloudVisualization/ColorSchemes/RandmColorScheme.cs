using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.TagsCloudVisualization.ColorSchemes
{
    public class RandomColorScheme : IColorScheme
    {
        private readonly Random random = new Random();

        Color IColorScheme.DefineColor(int frequency)
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
