using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualisation.Text.Formatting
{
    [VisibleName("Случайный шрифт")]
    public class RandomFontSource : IFontSource
    {
        private readonly Random random = new Random();
        private static readonly int[] sizes = Enumerable.Range(1, 10).Select(i => i * 2).ToArray();

        public IDictionary<string, Font> GetFontsForAll(WordWithFrequency[] allWords) =>
            allWords.ToDictionary(w => w.Word, _ => RandomFont());

        private Font RandomFont() => new Font(SystemFonts.DialogFont.FontFamily, sizes[random.Next(0, sizes.Length)]);
    }
}