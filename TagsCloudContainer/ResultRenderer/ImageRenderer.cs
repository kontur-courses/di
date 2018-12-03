using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloudContainer.ResultRenderer
{
    public class ImageRenderer : IResultRenderer
    {
        private readonly Image image;
        private readonly Graphics graphics;
        private readonly ImageFormat imageFormat;

        private bool DrawRectangles { get; set; }

        public ImageRenderer(Size imageSize, ImageFormat imageFormat)
        {
            if (imageSize.Width <= 0 || imageSize.Height <= 0)
            {
                throw new ArgumentException(
                    "Width and height of image have to be > 0",
                    nameof(imageSize));
            }

            this.imageFormat = imageFormat;

            image = new Bitmap(imageSize.Width, imageSize.Height);
            graphics = Graphics.FromImage(image);
            var background = new RectangleF(0, 0, image.Width, image.Height);
            graphics.FillRectangle(new SolidBrush(Color.Black), background);
        }

        public void Generate(IEnumerable<Word> words, string filename)
        {
            var center = new PointF(image.Width / 2f, image.Height / 2f);

            var centeredWords = words
                .Select(word =>
                {
                    var result = new Word(word.Font, word.Color, word.Value);
                    var location = new PointF(
                        word.Position.Location.X + center.X,
                        word.Position.Location.Y + center.Y);
                    result.Position = new RectangleF(location, word.Position.Size);

                    return result;
                });

            foreach (var word in centeredWords)
            {
                graphics.DrawString(
                    word.Value,
                    word.Font,
                    new SolidBrush(word.Color),
                    word.Position.Location);

                if (DrawRectangles)
                {
                    graphics.DrawRectangle(
                        new Pen(Color.Blue),
                        word.Position.X, word.Position.Y,
                        word.Position.Width, word.Position.Height);
                }
            }

            image.Save(filename, imageFormat);
        }

        public SizeF GetWordSize(Word word)
        {
            return graphics.MeasureString(word.Value, word.Font);
        }
    }
}