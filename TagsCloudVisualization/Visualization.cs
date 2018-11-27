using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization
{
    public class Visualization
    {
        private int defaultBitmapSide = 500;
        public Graphics graphics;
        public Size bitmapSize { get; private set; }
        public IEnumerable<Rectangle> rectangles;

        public void DetermineBitmapSizes()
        {
            var mostDistantRectangle = rectangles
                .OrderByDescending(rect => rect.GetCircumcircleRadius())
                .First();
            var circleRadius = mostDistantRectangle.GetCircumcircleRadius();
            var bitmapSide = Math.Max(circleRadius * 2, defaultBitmapSide);
            bitmapSize = new Size(bitmapSide, bitmapSide);
        }

        public Rectangle ShiftRectangleToCenter(Rectangle rect)
        {
            var layoutCenter = new Point(bitmapSize.Width / 2, bitmapSize.Height / 2);
            return new Rectangle(new Point(rect.X + layoutCenter.X, rect.Y + layoutCenter.Y), rect.Size);
        }

        public Bitmap GetTagCloudImage()
        {
            DetermineBitmapSizes();
            var canvas = new Bitmap(bitmapSize.Width, bitmapSize.Height);
            if (rectangles.Count() == 0)
                return canvas;
            using (graphics = Graphics.FromImage(canvas))
            {
                graphics.Clear(Color.White);
                DrawElements();
            }
            return canvas;
        }

        public virtual void DrawElements()
        {
            throw new NotImplementedException();
        }
    }
}
