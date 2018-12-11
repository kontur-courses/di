using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Visualizer.Settings.Colorizer
{
    public interface IColorizer
    {
        SolidBrush GetBrush(CloudItem item);
    }
}