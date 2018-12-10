using System;
using System.Drawing;

namespace TagCloudVisualization
{
    public class BasicDrawer : IWordDrawer
    {
        public void DrawWord(Graphics graphics, ImageCreatingOptions options, WordInfo wordInfo)
        {
            using (var font = new Font(options.FontName, 2, FontStyle.Regular))
            {
                var newFont = GetAdjustedFont(graphics, wordInfo.Word, font, wordInfo.Rectangle.Width, 100, 1);
                graphics.DrawString(wordInfo.Word, newFont, options.Brush, wordInfo.Rectangle);
            }
        }

        public bool Check(WordInfo word) => true;

        public Font GetAdjustedFont(
            Graphics graphics,
            string word,
            Font originalFont,
            int width,
            int maxFontSize,
            int minFontSize)
        {
            for (var adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                var testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style);

                var adjustedSizeNew = graphics.MeasureString(word, testFont);

                if (width > Convert.ToInt32(adjustedSizeNew.Width))
                    return testFont;
            }

            return originalFont;
        }
    }
}
