using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloud.TagsCloudVisualization.ColorSchemes
{
    public class RedColorScheme: IColorScheme
    {
        private  int maxDAlpha = 200;
        public Color DefineColor(int frequency)
        {
            return Color.FromArgb(255 - maxDAlpha/frequency, Color.Red);
        }
    }
}
