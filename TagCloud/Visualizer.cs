using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud
{
    public class Visualizer
    {
        private readonly Size bitmapSize;
        private readonly Pen pen;
        private readonly Point center;
        private readonly Color backGroundColor;

        public Visualizer(Size bitmapSize, string bgColor, string textColor)
        {
            this.bitmapSize = bitmapSize;
            this.backGroundColor = ColorTranslator.FromHtml(bgColor);
            pen = new Pen(ColorTranslator.FromHtml(textColor));
            center = new Point(bitmapSize.Width / 2, bitmapSize.Height / 2);
        }

        public void RenderCurrentConfig(ICloudLayouter cloud, string output)
        {
            var bitmap = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            using (var brush = new SolidBrush(backGroundColor))
            {
                graphics.FillRectangle(brush, 0, 0, bitmapSize.Width, bitmapSize.Height);
            }
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
