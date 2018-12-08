using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagsCloudDrawer : IDrawer
    {
        private const int CenterCircleDiameter = 10;

        private readonly Rectangle[] rectangles;
        private readonly Point center;
        private readonly int width;
        private readonly int height;
        private readonly int minX;
        private readonly int minY;
        private readonly Random random = new Random();

        public TagsCloudDrawer(Rectangle[] rectangles, Point center)
        {
            this.rectangles = rectangles;
            this.center = center;

            minX = rectangles.Select(r => r.Left).Min();
            var maxX = rectangles.Select(r => r.Right).Max();
            minY = rectangles.Select(r => r.Top).Min();
            var maxY = rectangles.Select(r => r.Bottom).Max();

            width = maxX - minX + 1;
            height = maxY - minY + 1;
        }

        public int GetWidth() => width;

        public int GetHeight() => height;

        public void Draw(Graphics graphics)
        {
            var centerCopy = new Point(center.X, center.Y);
            var offset = new Point(-minX, -minY);
            centerCopy.Offset(offset);

            graphics.FillRectangle(Brushes.White, 0, 0, width, height);
            foreach (var rectangle in rectangles)
            {
                graphics.FillRectangle(new SolidBrush(random.GetRandomColor()), rectangle.Move(offset));
            }

            centerCopy.Offset(-CenterCircleDiameter/2, -CenterCircleDiameter/2);
            graphics.FillEllipse(Brushes.Red, new Rectangle(centerCopy, new Size(CenterCircleDiameter, CenterCircleDiameter)));
        }
    }
}
