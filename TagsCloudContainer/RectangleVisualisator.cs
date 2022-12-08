using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudContainer
{
    public class RectangleVisualisator
    {
        private Bitmap _bitmap;
        private readonly Size shiftToBitmapCenter;
        private readonly List<Rectangle> _rectangles;
        private readonly Dictionary<string, int> _words;

        public RectangleVisualisator(Dictionary<string, int> words, CircularCloudLayouter layouter)
        {
            _words = words;
            _rectangles = layouter.GenerateRectanglesByWords(_words);
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
            var count = 0;
            using var pen = new Pen(Color.Purple);
            using var settingFont = new Font("Arial", 16);
            foreach (var word in _words)
            {
                var rectangleOnMap = CreateRectangleOnMap(_rectangles[count]);
                using var font = new Font(settingFont.FontFamily,
                    word.Value / (float) _words.Count * 100 * settingFont.Size);
                graphics.DrawString(word.Key, font, pen.Brush, rectangleOnMap.Location);
                count++;
            }
        }

        private Rectangle CreateRectangleOnMap(Rectangle rectangle)
        {
            return new Rectangle(rectangle.Location + shiftToBitmapCenter, rectangle.Size);
        }

        public void Save(string path, string filename, ImageFormat format)
        {
            _bitmap.Save($"{path}\\{filename}.{format}", format);
        }
    }
}