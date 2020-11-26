using System;
using System.Drawing;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public sealed class WordsVisualiser : BaseCloudVisualiser
    {
        private WordToDraw? currentWord;
        private readonly int scale;

        public WordsVisualiser(Point sourceCenterPoint, int scale) : base(sourceCenterPoint)
        {
            this.scale = scale;
            OnDraw += rect =>
            {
                var word = currentWord ?? throw new NullReferenceException($"{nameof(currentWord)} is null");
                Graphics.DrawString(word.Word, word.Font, word.Brush, rect);
                currentWord = null;
            };
        }

        public void Draw(Rectangle position, WordToDraw toDraw)
        {
            toDraw = new WordToDraw(toDraw.Word, WordToDraw.MultiplyFontSize(toDraw.Font, scale), toDraw.Brush);
            var wordSize = MeasureString(toDraw);

            if (wordSize.Height > position.Size.Height || wordSize.Width > position.Size.Width)
                throw new ArgumentException("Actual word size is larger than computed values");

            var offset = (wordSize - position.Size) / 2;
            var wordPosition = new RectangleF(position.X - offset.Width, position.Y - offset.Height,
                wordSize.Width, wordSize.Height);

            currentWord = toDraw;
            PrepareAndDraw(wordPosition);
        }

        public SizeF MeasureString(WordToDraw toDraw, int wordScale = 1)
        {
            if (Graphics != null)
                return MeasureString(Graphics, toDraw, wordScale);
            using var graphics = Graphics.FromHwnd(IntPtr.Zero);
            return MeasureString(graphics, toDraw, wordScale);
        }

        private static SizeF MeasureString(Graphics graphics, WordToDraw toDraw, int scale) =>
            graphics.MeasureString(toDraw.Word, WordToDraw.MultiplyFontSize(toDraw.Font, scale));
    }
}