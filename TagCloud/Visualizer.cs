using System.Drawing;
using System.Linq;
using TagCloud.CloudLayouter;
using TagCloud.PointGenerator;
using TagCloud.Templates;

namespace TagCloud
{
    public class Visualizer
    {
        private static readonly Brush Brush = Brushes.Black;
        private ICloudLayouter cloudLayouter;
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;

        public Visualizer(ICloudLayouter cloudLayouter) : this(cloudLayouter.SizeF.ToSize(), Color.Bisque)
        {
            this.cloudLayouter = cloudLayouter;
        }

        public Visualizer(Size size, Color backgroundColor)
        {
            cloudLayouter =
                new TagCloud.CloudLayouter.CloudLayouter(new Spiral(0.01f, 2, new PointF(size.Width / 2f, size.Height / 2f), new Cache()));
            bitmap = new Bitmap(size.Width, size.Height);
            graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(backgroundColor), 0, 0, size.Width, size.Height);
        }

        public void DrawRectangles(string file)
        {
            var cloudWithOffsetLocation = cloudLayouter.GetCloud()
                .Select(r =>
                    new RectangleF(
                        new PointF(r.X + cloudLayouter.SizeF.Width / 2, r.Y + cloudLayouter.SizeF.Height / 2),
                        r.Size)).ToArray();
            graphics.FillRectangles(new SolidBrush(Color.Coral), cloudWithOffsetLocation);
            graphics.DrawRectangles(new Pen(Color.Firebrick, 1f), cloudWithOffsetLocation);
            bitmap.Save(file);
        }

        public void Draw(Template template, string filename)
        {
            var bitmapCenter = new PointF(bitmap.Width / 2f, bitmap.Height / 2f);
            var offset = new PointF(bitmapCenter.X - template.Center.X, bitmapCenter.Y - template.Center.Y);
            foreach (var wordParameter in template.GetWords())
            {
                var rectangleF = wordParameter.WordRectangleF;
                rectangleF.Offset(offset);
                graphics.DrawString(wordParameter.Word, wordParameter.Font, Brush, rectangleF);
            }

            bitmap.Save(filename);
        }
    }
}