using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.Drawer
{
    public class CloudDrawer: ICloudDrawer
    {
        private readonly Pen pen;

        public CloudDrawer(Color color)
        {
            pen = new Pen(color);
        }

        public void Draw(Graphics graphics, Rectangle[] rectangles)
        {
            graphics.Clear(Color.White);
            graphics.DrawRectangles(pen, rectangles);
        }
    }
}
