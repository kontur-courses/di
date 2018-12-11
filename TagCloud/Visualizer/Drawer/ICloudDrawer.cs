using System.Collections.Generic;
using System.Drawing;
using TagCloud.Models;
using TagCloud.Visualizer.Settings;

namespace TagCloud.Visualizer.Drawer
{
    public interface ICloudDrawer
    {
        void Draw(Graphics graphics, IList<CloudItem> cloudItems, IDrawSettings settings);
    }
}