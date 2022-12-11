using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class Drawer
    {
        public Bitmap DrawWords(List<TextContainer> containers, int radius, Color background, Color font)
        {
            radius += 100;
            var bitmap = new Bitmap(radius * 2, radius * 2);
            var g = Graphics.FromImage(bitmap);
            g.Clear(background);
            foreach (var container in containers)
            {
                int x = container.Point.X + radius;
                int y = container.Point.Y + radius;
                g.DrawString(container.Text, container.Font, new SolidBrush(font), x, y);
            }
            return bitmap;
        }
    }
}