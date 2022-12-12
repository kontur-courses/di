using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using TagsCloudContainer.WorkWithWords;

namespace TagsCloudContainer.Visualisators
{
    public class RectangleVisualisator : IVisualisator
    {
        private Bitmap _bitmap;
        private readonly Size shiftToBitmapCenter;
        private readonly List<Rectangle> _rectangles;
        private readonly List<Word> _words;
        private readonly Settings _settings;

        public RectangleVisualisator(WordHandler handler, CircularCloudLayouter layouter, Settings settings)
        {
            _words = handler.ProcessWords();
            _settings = settings;
            _rectangles = WordGenerator.GenerateRectanglesByWords(_words, layouter, settings);
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
            using var pen = new Pen(_settings.WordColor);
            foreach (var word in _words)
            {
                var rectangleOnMap = CreateRectangleOnMap(_rectangles[count]);
                using var font = new Font(_settings.WordFontName, word.Size);
                graphics.DrawString(word.Value, font, pen.Brush, rectangleOnMap.Location);
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