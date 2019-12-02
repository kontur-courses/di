using System;
using System.Drawing;
using System.Linq;
using TagsCloudGenerator.Interfaces;

namespace TagsCloudGenerator.PaintersAndSavers
{
    public class DefaultPainterAndSaver : IPainterAndSaver
    {
        private readonly IPainter painter;
        private readonly ISaver saver;

        public DefaultPainterAndSaver(IPainter painter, ISaver saver)
        {
            this.painter = painter ?? throw new ArgumentNullException(nameof(painter));
            this.saver = saver ?? throw new ArgumentNullException(nameof(saver)); 
        }

        public bool TryPaintAndSave((string word, Font font, RectangleF wordRectangle)[] layoutedWords, Size imageSize, string pathToSave)
        {
            if (layoutedWords == null)
                throw new ArgumentNullException(nameof(layoutedWords));
            if (imageSize.Width < 1 || imageSize.Height < 1)
                throw new ArgumentOutOfRangeException(nameof(imageSize));

            var size = CalculateImageSizeAndChangeRectanglesPositions(layoutedWords);
            using (var bm = new Bitmap(size.Width, size.Height))
            {
                using (var gr = Graphics.FromImage(bm))
                    painter.DrawWords(layoutedWords, gr);
                using (var b = new Bitmap(bm, imageSize))
                    if (!saver.TrySaveTo(pathToSave, b))
                        return false;
            }
            return true;
        }

        private Size CalculateImageSizeAndChangeRectanglesPositions(
            (string word, Font font, RectangleF wordRectangle)[] layoutedWords)
        {
            const int frameWidth = 100;
            if (layoutedWords.Length == 0)
                return new Size(frameWidth * 2, frameWidth * 2);

            var minX = layoutedWords.Min(r => r.wordRectangle.Left);
            var minY = layoutedWords.Min(r => r.wordRectangle.Top);
            var maxX = layoutedWords.Max(r => r.wordRectangle.Right);
            var maxY = layoutedWords.Max(r => r.wordRectangle.Bottom);

            var lengthByX = maxX - minX + 1;
            var lengthByY = maxY - minY + 1;

            var imageSize = new Size((int)lengthByX + 2 * frameWidth, (int)lengthByY + 2 * frameWidth);

            for (var i = 0; i < layoutedWords.Length; i++)
            {
                layoutedWords[i].wordRectangle.Location = new PointF(
                    x: layoutedWords[i].wordRectangle.X - minX + frameWidth,
                    y: layoutedWords[i].wordRectangle.Y - minY + frameWidth);
            }

            return imageSize;
        }
    }
}