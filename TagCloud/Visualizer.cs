using System.Drawing;
using TagCloud.Templates;

namespace TagCloud
{
    public class Visualizer : IVisualizer
    {
        public void Draw(ITemplate template, string filename)
        {
            var bitmap = new Bitmap(template.Size.Width, template.Size.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(template.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);
            var bitmapCenter = new PointF(bitmap.Width / 2f, bitmap.Height / 2f);
            var offset = new PointF(bitmapCenter.X - template.Center.X, bitmapCenter.Y - template.Center.Y);
            foreach (var wordParameter in template.GetWordParameters())
            {
                var rectangleF = wordParameter.WordRectangleF;
                rectangleF.Offset(offset);
                graphics.DrawString(wordParameter.Word, wordParameter.Font, new SolidBrush(wordParameter.Color),
                    rectangleF);
            }

            bitmap.Save(filename);
        }
    }
}