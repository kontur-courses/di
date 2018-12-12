using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.ResultRenderer
{
    public class ImageRenderer : IResultRenderer
    {
        private readonly Size imageSize;

        public bool DrawRectangles { get; set; }

        public ImageRenderer(IResultRendererConfig config)
        {
            imageSize = config.ImageSize;

            if (imageSize.Width <= 0 || imageSize.Height <= 0)
            {
                throw new ArgumentException(
                    "Width and height of image have to be > 0",
                    nameof(imageSize));
            }
        }

        public Image Generate(IEnumerable<Word> words)
        {
            if (words == null)
            {
                throw new ArgumentException("Given words can't be null", nameof(words));
            }

            var center = new PointF(imageSize.Width / 2f, imageSize.Height / 2f);

            var centeredWords = CenterWords(center, words);

            return GenerateImage(centeredWords);
        }

        private Image GenerateImage(IEnumerable<Word> words)
        {
            var image = new Bitmap(imageSize.Width, imageSize.Height);

            using (var graphics = Graphics.FromImage(image))
            {
                var background = new RectangleF(0, 0, image.Width, image.Height);
                graphics.FillRectangle(new SolidBrush(Color.Black), background);

                foreach (var word in words)
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
            }

            return image;
        }

        private IEnumerable<Word> CenterWords(PointF center, IEnumerable<Word> words)
        {
            return words
                .Select(word =>
                {
                    var result = new Word(word.Font, word.Color, word.Value);
                    var location = new PointF(
                        word.Position.Location.X + center.X,
                        word.Position.Location.Y + center.Y);
                    result.Position = new RectangleF(location, word.Position.Size);

                    return result;
                });
        }
    }
}