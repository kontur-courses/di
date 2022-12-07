using System;
using System.Drawing;
using System.Collections.Generic;

namespace TagsCloudContainer
{
    public class CircularCloudLayouter
    {
        private Spiral _spiral;
        private List<Rectangle> _rectangles;
        private Point _center;
        
        public CircularCloudLayouter(Point center)
        {
            _center = center;
            _spiral = new Spiral(center, 2);
            _rectangles = new List<Rectangle>();
        }
        
        public List<Rectangle> GenerateRectanglesByWords(Dictionary<string, int> words)
        {
            List<Rectangle> rectangles = new List<Rectangle>();
            using var settingFont = new Font("Arial", 16);
            foreach (var word in words)
            {
                using var font = new Font(settingFont.FontFamily, word.Value / (float)words.Count * 100 * settingFont.Size);
                var size = MeasureWord(word.Key, font);
                rectangles.Add(PutNextRectangle(size));
            }
            
            return rectangles;
        }

        private static Size MeasureWord(string word, Font font)
        {
            using var bitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bitmap);
            var result = graphics.MeasureString(word, font);
            result = result.ToSize();
            if (result.Width == 0) result.Width = 1;
            if (result.Height == 0) result.Height = 1;
            return result.ToSize();
        }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (rectangleSize == Size.Empty || rectangleSize.Height < 0 || rectangleSize.Width < 0)
                throw new ArgumentException("The size must not be equal to or less than 0");
            
            Rectangle rectangle;
            do
            {
                rectangle = new Rectangle(_spiral.NextPoint(), rectangleSize);
            } while (rectangle.IsIntersects(_rectangles));
            
            if(_rectangles.Count != 0)
                rectangle = ShiftRectangleToCenter(rectangle);
            
            _rectangles.Add(rectangle);
            return rectangle;
        }

        private Rectangle ShiftRectangleToCenter(Rectangle rectangle)
        {
            var dx = rectangle.GetCenter().X < _center.X ? 1 : -1;
            rectangle = ShiftRectangle(rectangle, dx, 0);
            var dy = rectangle.GetCenter().Y < _center.Y ? 1 : -1;
            rectangle = ShiftRectangle(rectangle, 0, dy);
            return rectangle;
        }

        private Rectangle ShiftRectangle(Rectangle rectangle, int dx, int dy)
        {
            var offset = new Size(dx, dy);
            while (rectangle.IsIntersects(_rectangles) == false &&
                   rectangle.GetCenter().X != _center.X &&
                   rectangle.GetCenter().Y != _center.Y)
                rectangle.Location += offset;

            if (rectangle.IsIntersects(_rectangles))
                rectangle.Location -= offset;

            return rectangle;
        }
    }
}