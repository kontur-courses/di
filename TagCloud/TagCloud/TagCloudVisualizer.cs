using System.Drawing;

namespace TagCloud
{
    public class TagCloudVisualizer : IVisualizer
    {
        private readonly ITagCloud tagCloud;
        private readonly Pen[] pens;

        public TagCloudVisualizer(ITagCloud tagCloud)
        {
            this.tagCloud = tagCloud;
            pens = new Pen[7];
            pens[0] = new Pen(Color.Red);
            pens[1] = new Pen(Color.Orange);
            pens[2] = new Pen(Color.Yellow);
            pens[3] = new Pen(Color.Green);
            pens[4] = new Pen(Color.Teal);
            pens[5] = new Pen(Color.Blue);
            pens[6] = new Pen(Color.Purple);
        }

        private static PointF[] RectangleToPointFArray(Rectangle rectangle)
        {
            return new[]
            {
                new PointF(rectangle.Left, rectangle.Bottom),
                new PointF(rectangle.Right, rectangle.Bottom),
                new PointF(rectangle.Right, rectangle.Top),
                new PointF(rectangle.Left, rectangle.Top)
            };
        }

        public Bitmap CreateBitMap(int width, int height)
        {
            var bitMap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitMap);
            var penNumber = 0;
            foreach (var rectangle in tagCloud.Rectangles)
            {
                var pen = pens[penNumber];
                graphics.DrawPolygon(pen, RectangleToPointFArray(rectangle));
                penNumber++;
                penNumber %= pens.Length;
            }

            return bitMap;
        }
    }
}