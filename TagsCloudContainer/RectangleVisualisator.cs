using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudContainer
{
    public class RectangleVisualisator
    {
        private readonly Random _random;
        private Bitmap _bitmap;
        private readonly Size shiftToBitmapCenter;
        private readonly List<Rectangle> _rectangles;
        public RectangleVisualisator(List<Rectangle> rectangles)
        {
            _rectangles = rectangles;
            _random = new Random();
            _bitmap = GenerateBitmap();
            shiftToBitmapCenter = new Size(_bitmap.Width / 2, _bitmap.Height / 2);
        }

        private Bitmap GenerateBitmap()
        {
            var width = _rectangles.Max(rectangle => rectangle.Right) - 
                        _rectangles.Min(rectangle => rectangle.Left);
            
            var height = _rectangles.Max(rectangle => rectangle.Bottom) - 
                         _rectangles.Min(rectangle => rectangle.Top);

            return new Bitmap(width * 2, height * 2);
        }

        public void Paint()
        {
            using var graphics = Graphics.FromImage(_bitmap);
            graphics.Clear(Color.Black);

            foreach (var rectangle in _rectangles)
            {
                using var pen = new Pen(Color.FromArgb(_random.Next() % 255, _random.Next() % 255, _random.Next() % 255));
                var rectangleOnMap = CreateRectangleOnMap(rectangle);
                graphics.DrawRectangle(pen, rectangleOnMap);
            }
        }

        private Rectangle CreateRectangleOnMap(Rectangle rectangle)
        {
            return new Rectangle(rectangle.Location + shiftToBitmapCenter, rectangle.Size);
        }
        
        public void Save(string filename, ImageFormat format)
        {
            _bitmap.Save($"{filename}.{format}", format);
        }
    }
}