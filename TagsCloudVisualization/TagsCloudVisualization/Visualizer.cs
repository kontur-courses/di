using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Visualizer : IVisualizer
    {
        public IWordPalette Palette { get; set; }

        public Visualizer(IWordPalette palette)
        {
            Palette = palette;
        }

        public Image Render(IEnumerable<GraphicWord> words, int width, int height)
        {
            Palette.ColorWords(words);
            var image = new Bitmap(width, height);
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };
            var graphics = Graphics.FromImage(image);

            graphics.DrawImage(Palette.GetBackground(new Size(width, height)), new Point());
            foreach (var word in words)
            {
                graphics.DrawString(word.Value, word.Font,
                    new SolidBrush(word.Color),
                    new PointF(word.Rectangle.X + (word.Rectangle.Width / 2),
                        word.Rectangle.Y + (word.Rectangle.Height / 2)), stringFormat);
            }

            return image;
        }
    }
}
