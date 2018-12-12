using System;
using System.Drawing;

namespace TagsCloudContainer.ResultRenderer
{
    public class WordsSizer : IDisposable
    {
        private readonly Graphics graphics;
        private readonly Image image;

        public WordsSizer()
        {
            image = new Bitmap(1, 1);
            graphics = Graphics.FromImage(image);
        }

        public SizeF GetWordSize(Word word)
        {
            if (word == null)
            {
                throw new ArgumentException("Given word can't be null", nameof(word));
            }

            return graphics.MeasureString(word.Value, word.Font);
        }

        public void Dispose()
        {
            graphics?.Dispose();
            image?.Dispose();
        }
    }
}