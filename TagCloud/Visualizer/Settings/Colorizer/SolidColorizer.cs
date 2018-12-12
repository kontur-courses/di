using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Visualizer.Settings.Colorizer
{
    public class SolidColorizer : IColorizer
    {
        private Color color;

        public SolidColorizer(Color color)
        {
            this.color = color;
        }
        public Brush GetBrush(CloudItem item)
        {
            return new SolidBrush(color);
        }
    }
}