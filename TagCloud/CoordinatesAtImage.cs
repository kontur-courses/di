using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;

namespace TagsCloud
{
    public class CoordinatesAtImage
    {
        private readonly Size imageSize;

        public CoordinatesAtImage(Size imageSize)
        {
            this.imageSize = imageSize;
        }

        public IReadOnlyCollection<KeyValuePair<string, Rectangle>> GetCoordinates(IReadOnlyCollection<KeyValuePair<string, Rectangle>> words)
        {
            if (words.Count == 0)
                return words;
            var boundingCoordinate = new BoundingCoordinate(words.Select(x => x.Value).ToList());
            return GetWordsCoordinateAtImage(words, boundingCoordinate);
        }

        private Dictionary<string, Rectangle> GetWordsCoordinateAtImage(IReadOnlyCollection<KeyValuePair<string, Rectangle>> words,
            BoundingCoordinate bounds)
        {
            var coefficientWidth = imageSize.Width * 1.0 / bounds.SizeX;
            var coefficientHeight = imageSize.Height * 1.0 / bounds.SizeY;
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