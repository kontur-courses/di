using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    public class RandomWordFormatter : IWordFormatter
    {
        private static readonly KnownColor[] colors = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToArray();

        private readonly Random random = new Random();

        public Font GetFont(string word, int totalWordsCount, int positionInTop)
        {
            return new Font(SystemFonts.DialogFont.FontFamily, random.Next(3, 10));
        }

        public Brush GetBrush(string word, double distanceFromCenter)
        {
            var index = random.Next(0, colors.Length);
            return new SolidBrush(Color.FromKnownColor(colors[index]));
        }
    }
}