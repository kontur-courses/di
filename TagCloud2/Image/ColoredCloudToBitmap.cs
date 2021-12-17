using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud2.Image
{
    public class ColoredCloudToBitmap : IColoredCloudToImageConverter
    {
        public System.Drawing.Image GetImage(IColoredCloud cloud, int xSize, int ySize)
        {
            var words = cloud.ColoredWords;
            var bitmap = new Bitmap(xSize, ySize);
            var g = Graphics.FromImage(bitmap);
            g.Clear(Color.Black);
            foreach (var r in words)
            {
                var word = r.Word;
                var brush = new SolidBrush(r.Color);
                g.DrawString(word, r.Font, brush, r.Size);
            }
            return bitmap;
        }
    }
}
