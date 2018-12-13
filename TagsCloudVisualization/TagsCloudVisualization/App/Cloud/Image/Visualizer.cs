using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Visualizer : IVisualizer
    {
        public Image Render(IEnumerable<GraphicWord> words, int width, int height, IWordPalette palette)
        {
            palette.ColorWords(words);
            var image = new Bitmap(width, height);
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center
            };
            var graphics = Graphics.FromImage(image);

            graphics.DrawImage(palette.GetBackground(new Size(width, height)), new Point());
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
