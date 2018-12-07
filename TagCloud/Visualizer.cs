using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud
{
    public class Visualizer
    {
        private readonly Size bitmapSize;
        private readonly Pen pen;
        private readonly Point center;

        public Visualizer(Size bitmapSize)
        {
            this.bitmapSize = bitmapSize;
            pen = new Pen(Color.Black);
            center = new Point(bitmapSize.Width / 2, bitmapSize.Height / 2);
        }

        public void RenderCurrentConfig(ICloudLayouter cloud, string output)
        {
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            foreach (var rect in cloud.Rectangles)
            {
                var replacedRect = new Rectangle(rect.Key.Location.X + center.X,
                    rect.Key.Location.Y + center.Y, rect.Key.Size.Width, rect.Key.Height);
                graphics.DrawString(rect.Value, new Font(FontFamily.GenericSerif, rect.Key.Height), 
                    pen.Brush, replacedRect.Location);
            }
            bitmap.Save(output);
        }
    }
}
