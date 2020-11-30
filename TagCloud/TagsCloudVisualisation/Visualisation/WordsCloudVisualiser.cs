using System;
using System.Drawing;
using TagsCloudVisualisation.Text;
using TagsCloudVisualisation.Text.Formatting;

namespace TagsCloudVisualisation.Visualisation
{
    public sealed class WordsCloudVisualiser : BaseCloudVisualiser
    {
        private FormattedWord? currentWord;

        public WordsCloudVisualiser(Point sourceCenterPoint) : base(sourceCenterPoint)
        {
            OnDraw += rect =>
            {
                var word = currentWord ?? throw new NullReferenceException($"{nameof(currentWord)} is null");
                Graphics.DrawString(word.Word, word.Font, word.Brush, rect);
                currentWord = null;
            };
        }

        public void Draw(Rectangle position, FormattedWord toDraw)
        {
            var wordSize = MeasureString(toDraw.Word, toDraw.Font);

            if (wordSize.Height > position.Size.Height || wordSize.Width > position.Size.Width)
                throw new ArgumentException("Actual word size is larger than computed values");

            var offset = (wordSize - position.Size) / 2;
            var wordPosition = new RectangleF(position.X - offset.Width, position.Y - offset.Height,
                wordSize.Width, wordSize.Height);

            currentWord = toDraw;
            PrepareAndDraw(wordPosition);
        }

        public SizeF MeasureString(string word, Font font)
        {
            if (Graphics != null)
                return MeasureString(Graphics, word, font);
            using var graphics = Graphics.FromHwnd(IntPtr.Zero);
            return MeasureString(graphics, word, font);
        }

        private static SizeF MeasureString(Graphics graphics, string word, Font font) =>
            graphics.MeasureString(word, font);
    }
}