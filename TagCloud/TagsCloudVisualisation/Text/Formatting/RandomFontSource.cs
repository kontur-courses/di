using System;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случайный шрифт")]
    public class RandomFontSource : IFontSource
    {
        private readonly Random random = new Random();
        private static readonly int[] sizes = Enumerable.Range(1, 10).Select(i => i * 2).ToArray();

        public Font GetFont(string word, int totalWordsCount, int positionInTop)
        {
            return new Font(SystemFonts.DialogFont.FontFamily, sizes[random.Next(0, sizes.Length)]);
        }
    }
}