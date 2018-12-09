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
                var newFont = GetAdjustedFont(graphics, wordInfo.Word, font, wordInfo.Rectangle.Width, 100, 1, false);
                graphics.DrawString(wordInfo.Word, newFont, options.Brush, wordInfo.Rectangle);
            }
        }
        public Font GetAdjustedFont(Graphics graphicRef, string graphicString, Font originalFont, int containerWidth, int maxFontSize, int minFontSize, bool smallestOnFail)
        {
            Font testFont = null;
            // We utilize MeasureString which we get via a control instance           
            for (int adjustedSize = maxFontSize; adjustedSize >= minFontSize; adjustedSize--)
            {
                testFont = new Font(originalFont.Name, adjustedSize, originalFont.Style); 

                // Test the string with the new size
                var adjustedSizeNew = graphicRef.MeasureString(graphicString, testFont);

                if (containerWidth > Convert.ToInt32(adjustedSizeNew.Width))
                {
                    // Good font, return it
                    return testFont;
                }
            }

            // If you get here there was no fontsize that worked
            // return minimumSize or original?
            return smallestOnFail ? testFont : originalFont;
        }
        public bool Check(WordInfo word) => true;
    }
}