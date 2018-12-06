using System;
using System.Drawing;
using TagsCloudVisualization;

namespace TagCloud
{
    public class Visualiser
    {
        private readonly Size bitmapSize;
        private readonly Pen pen;
        private readonly Point center;
        private readonly string output;

        public Visualiser(Size bitmapSize, string output)
        {
            this.bitmapSize = bitmapSize;
            this.output = output;
            pen = new Pen(Color.Black);
            center = new Point(bitmapSize.Width / 2, bitmapSize.Height / 2);
        }

        public void RenderCurrentConfig(CircularCloudLayouter cloud)
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

        private Size GetNewRectangleSize(Random random)
        {
            var minWidth = 10;
            var minHeight = 5;
            var rectangleSize = new Size
            {
                Width = minWidth + (int) (random.NextDouble() * 50),
                Height = minHeight + (int) (random.NextDouble() * 20)
            };
            return rectangleSize;
        }

        public void MakeExampleImage(int rectangleCount, Size maxRectangleSize)
        {
            var cloud = new CircularCloudLayouter(new Point(0, 0), 0.5);
            var random = new Random();
            for (var i = 0; i < rectangleCount; i++)
            {
                var currentRectangleSize = GetNewRectangleSize(random);
                cloud.PutNextRectangle(currentRectangleSize);
            }
            RenderCurrentConfig(cloud);
        }
    }
}
