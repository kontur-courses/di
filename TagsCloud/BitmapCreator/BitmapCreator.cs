using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloud.Extensions;
using TagsCloud.ImageConfig;
using TagsCloud.LayoutAlgorithms;

namespace TagsCloud.BitmapCreator
{
    public class BitmapCreator : IBitmapCreator
    {
        private readonly ILayoutAlgorithm _algorithm;
        private readonly IImageConfig _imageConfig;
        private Bitmap _bitmap;
        private Graphics _graphics;

        public BitmapCreator(ILayoutAlgorithm algorithm, IImageConfig imageConfig)
        {
            _algorithm = algorithm;
            _imageConfig = imageConfig;
        }

        public Bitmap Create(IEnumerable<string> words)
        {
            _bitmap = new Bitmap(_imageConfig.Size.Width, _imageConfig.Size.Height);
            _graphics = Graphics.FromImage(_bitmap);
            var frequency = words.GetFrequency().OrderByDescending(x => x.Value).ToArray();
            _graphics.FillRectangle(new SolidBrush(_imageConfig.BackgroundColor), 0,0, _imageConfig.Size.Width, _imageConfig.Size.Height);
            if (!words.Any())
                return _bitmap;
            var a = _imageConfig.Size.Width * 0.76;
            foreach (var (word, freq) in frequency)
            {
                var fontSize = Math.Max((int)(freq * a), 10);
                var font = new Font(_imageConfig.FontFamily, fontSize);
                var sizeF = _graphics.MeasureString(word, font);
                var width = (int) Math.Ceiling(sizeF.Width);
                var height = (int) Math.Ceiling(sizeF.Height);
                var rectangle = _algorithm.PutNextRectangle(new Size(width, height));
                _graphics.DrawRectangle(new Pen(Color.Black), rectangle);
                _graphics.DrawString(word, font, new SolidBrush(_imageConfig.ColoringAlgorithm.GetNextColor()),
                    rectangle);
            }
            return _bitmap;
        }

        public void Dispose()
        {
            _graphics.Dispose();
            _bitmap.Dispose();
        }
    }
}
