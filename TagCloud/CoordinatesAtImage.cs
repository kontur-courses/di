using System;
using System.Collections.Generic;
using System.Drawing;
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

        public IReadOnlyCollection<Tag> GetCoordinates(IReadOnlyCollection<Tag> words)
        {
            if (words.Count == 0)
                return words;
            var boundingCoordinate = new BoundingCoordinate(words.Select(x => x.WordBox).ToList());
            return GetWordsCoordinateAtImage(words, boundingCoordinate);
        }

        private IReadOnlyCollection<Tag> GetWordsCoordinateAtImage(IReadOnlyCollection<Tag> words,
            BoundingCoordinate bounds)
        {
            var coefficientWidth = imageSize.Width * 1.0 / bounds.SizeX;
            var coefficientHeight = imageSize.Height * 1.0 / bounds.SizeY;
            var proportionalityCoefficient = Math.Min(coefficientHeight, coefficientWidth);
            var newCenter = new Size(imageSize.Width / 2, imageSize.Height / 2);
            var newCoordinate = new List<Tag>();
            foreach (var keyValuePair in words)
            {
                var newX = keyValuePair.WordBox.X * proportionalityCoefficient + newCenter.Width;
                var newY = keyValuePair.WordBox.Y * proportionalityCoefficient + newCenter.Height;
                var newWidth = keyValuePair.WordBox.Width * proportionalityCoefficient;
                var newHeight = keyValuePair.WordBox.Height * proportionalityCoefficient;
                var newRectangle = new Rectangle((int) newX, (int) newY, (int) newWidth, (int) newHeight);
                var tag = new Tag(keyValuePair.Word, newRectangle);
                newCoordinate.Add(tag);
            }

            return newCoordinate;
        }
    }
}