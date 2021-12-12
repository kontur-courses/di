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
            var words = cloud.GetColoredWords();
            var bitmap = new Bitmap(xSize, ySize);
            var g = Graphics.FromImage(bitmap);
            foreach (var r in words)
            {
                var word = r.GetWord();
                var brush = new SolidBrush(r.GetColor());
                g.DrawString(word, r.GetFont(), brush, r.GetSize());
            }
            return bitmap;
        }
    }
}
