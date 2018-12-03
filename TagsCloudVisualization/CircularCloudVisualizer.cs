using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class CircularCloudVisualizer
    {
        public Pen Pen { get; set; }

        public CircularCloudVisualizer(Pen pen)
        {
            Pen = pen;
        }
        public Bitmap DrawRandomRectanglesInBitmap(int minValue, int maxValue, Point center, int numberRectangles)
        {
            var cloud = new CircularCloudLayouter(center);
            var bmp = new Bitmap(cloud.WindowSize.Width, cloud.WindowSize.Height);
            var gr = Graphics.FromImage(bmp);
            gr.Clear(Color.AliceBlue);
            var rnd = new Random();
            for (int i = 0; i < numberRectangles; i++)
            {
                var size = new Size(rnd.Next(minValue, maxValue), rnd.Next(minValue, maxValue));
                Rectangle rect = cloud.PutNextRectangle(size);
                gr.DrawRectangle(Pen, rect);
            }
            gr.DrawEllipse(Pen, center.X, center.Y, 3, 3);
            return bmp;
        }

        public Bitmap DrawCircularCloud(CircularCloudLayouter cloud)
        {
            var bmp = new Bitmap(cloud.WindowSize.Width, cloud.WindowSize.Height);
            var gr = Graphics.FromImage(bmp);
            gr.Clear(Color.AliceBlue);
            cloud.Rectangles.ForEach(rect => gr.DrawRectangle(Pen, rect));
            return bmp;
        }
    }
}