using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.WorkWithWords
{
    public static class WordGenerator
    {
        public static List<Rectangle> GenerateRectanglesByWords(List<Word> words, CircularCloudLayouter layouter,
            Settings settings)
        {
            var rectangles = new List<Rectangle>();
            foreach (var word in words)
            {
                using var font = new Font(settings.WordFontName, word.Size);
                var size = MeasureWord(word.Value, font);
                rectangles.Add(layouter.PutNextRectangle(size));
            }

            return rectangles;
        }

        private static Size MeasureWord(string word, Font font)
        {
            using var bitmap = new Bitmap(1, 1);
            using var graphics = Graphics.FromImage(bitmap);
            var result = graphics.MeasureString(word, font);
            result = result.ToSize();
            if (result.Width == 0) result.Width = 1;
            if (result.Height == 0) result.Height = 1;
            return result.ToSize();
        }
    }
}