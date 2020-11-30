using System;
using System.Drawing;
using System.Collections.Generic;
using TagsCloudContainer.Layouter;

namespace TagsCloudContainer.Drawer
{
    public class LayoutDrawer : ILayoutDrawer
    {
        private readonly Bitmap bitmap;
        private readonly Graphics graphics;
        private readonly Random random;
        private readonly IOptions options;
        private IEnumerable<WordRectangle> rectangles;

        Graphics ILayoutDrawer.Graphics => graphics;
        Bitmap ILayoutDrawer.Bitmap => bitmap;

        public LayoutDrawer(IOptions options)
        {
            this.options = options;
            bitmap = new Bitmap(options.Width, options.Height);
            graphics = Graphics.FromImage(bitmap);
            random = new Random();
        }

        public void AddRectangles(IEnumerable<WordRectangle> rectangles)
        {
            this.rectangles = rectangles;
        }

        public void Draw()
        {
            foreach (var rectangle in rectangles)
            {
                var brush = new SolidBrush(Color.FromArgb(random.Next(255), random.Next(255), random.Next(255)));
                using var arialFont = new Font(options.Font, rectangle.FontSize);
                graphics.DrawString(rectangle.Text, arialFont, brush, rectangle.Rectangle.Location);
            }
        }
    }
}