using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Painter
    {
        private readonly Bitmap field;
        private readonly Graphics image;
        private Brush brush;
        
        public Painter(Size size)
        {
            field = new Bitmap(size.Width, size.Height);
            image = Graphics.FromImage(field);
            image.Clear(Color.White);
            brush = new SolidBrush(Color.Black);
        }
        
        public Bitmap GetSingleColorCloud(Color color, IEnumerable<Rectangle> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                brush = new SolidBrush(color);
                image.FillRectangle(brush, rectangle);
            }
            
            return field;
        }
        
        public Bitmap GetMultiColorCloud(IEnumerable<Rectangle> rectangles)
        {
            foreach (var rectangle in rectangles)
            {
                brush = new SolidBrush(GetRandomColor());
                image.FillRectangle(brush, rectangle);
            }

            return field;
        }
        
        private Color GetRandomColor()
        {
            var random = new Random();
            return Color.FromArgb(random.Next(256),
                random.Next(256), random.Next(256));
        }
    }
}