using System;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudLibraryTests
{
    public class TagsCloudImage
    {
        private readonly Bitmap _image;
        private readonly Graphics _graphics;
        private readonly int _width;
        private readonly int _height;
        private readonly Random _random = new Random();

        public TagsCloudImage(int width, int height)
        {
            _width = width;
            _height = height;
            _image = new Bitmap(width, height);
            _graphics = Graphics.FromImage(_image);
        }

        public void AddRectangles(List<Rectangle> rectangles, float penWidth = 2f)
        {
            foreach (var rectangle in rectangles)
            {
                var adjustedRectangle = new Rectangle(
                    rectangle.X + _width / 2,
                    rectangle.Y + _height / 2,
                    rectangle.Width,
                    rectangle.Height
                );
                var randomColor = Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
                _graphics.DrawRectangle(new Pen(randomColor, penWidth), adjustedRectangle);
            }
        }

        public void AddRectangles(List<Rectangle> rectangles, Color penColor, float penWidth = 2f)
        {
            var pen = new Pen(penColor, penWidth);
            foreach (var rectangle in rectangles)
            {
                var adjustedRectangle = new Rectangle(
                    rectangle.X + _width / 2,
                    rectangle.Y + _height / 2,
                    rectangle.Width,
                    rectangle.Height
                );
                _graphics.DrawRectangle(pen, adjustedRectangle);
            }
        }

        public Bitmap GetBitmap()
        {
            return _image.Clone() as Bitmap;
        }
    }
}
