using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud
{
    public class Graphics
    {
        private readonly Color fontColor;
        private readonly string fontFamily;
        private readonly Size imageSize;
        private readonly string nameOfImage;

        public Graphics(string nameOfImage, string fontFamily, Color fontColor, Size imageSize)
        {
            this.nameOfImage = nameOfImage;
            this.fontFamily = fontFamily;
            this.fontColor = fontColor;
            this.imageSize = imageSize;
        }

        public void SaveMap(Dictionary<string, Rectangle> words)
        {
            if (words.Count == 0)
                return;
            var boundingCoordinate = new BoundingCoordinate(words);
            var drawFormat = new StringFormat
            {
                FormatFlags = StringFormatFlags.DirectionRightToLeft,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var coordinateAtImage = GetWordsCoordinateAtImage(words, boundingCoordinate);
            using (var map = new Bitmap(imageSize.Width, imageSize.Height))
            using (var graphics = System.Drawing.Graphics.FromImage(map))
            {
                foreach (var keyValuePair in coordinateAtImage)
                    using (var brush = new SolidBrush(fontColor))
                    {
                        var currentFont = new Font(fontFamily, keyValuePair.Value.Height);
                        graphics.DrawString(keyValuePair.Key, currentFont, brush, keyValuePair.Value, drawFormat);
                    }

                map.Save($"{nameOfImage}.png", ImageFormat.Png);
            }
        }


        private Dictionary<string, Rectangle> GetWordsCoordinateAtImage(Dictionary<string, Rectangle> words,
            BoundingCoordinate bounds)
        {
            var currentHeight = bounds.MaxY - bounds.MinY;
            var currentWidth = bounds.MaxX - bounds.MinX;
            var coefficientWidth = imageSize.Width * 1.0 / currentWidth;
            var coefficientHeight = imageSize.Height * 1.0 / currentHeight;
            var proportionalityCoefficient = Math.Min(coefficientHeight, coefficientWidth);
            var newCenter = new Size(imageSize.Width / 2, imageSize.Height / 2);
            var newCoordinate = new Dictionary<string, Rectangle>();
            foreach (var keyValuePair in words)
            {
                var newX = keyValuePair.Value.X * proportionalityCoefficient + newCenter.Width;
                var newY = keyValuePair.Value.Y * proportionalityCoefficient + newCenter.Height;
                var newWidth = keyValuePair.Value.Width * proportionalityCoefficient;
                var newHeight = keyValuePair.Value.Height * proportionalityCoefficient;
                var newRectangle = new Rectangle((int) newX, (int) newY, (int) newWidth, (int) newHeight);
                newCoordinate.Add(keyValuePair.Key, newRectangle);
            }

            return newCoordinate;
        }
    }
}