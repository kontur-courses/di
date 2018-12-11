using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Visualizer.Settings.Colorizer
{
    public class SolidColorizer : IColorizer
    {
        private readonly Color color;

        public SolidColorizer(Color color)
        {
            this.color = color;
        }
        public SolidBrush GetBrush(CloudItem item)
        {
            return new SolidBrush(color);
        }
    }
}