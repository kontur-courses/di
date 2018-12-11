using System;
using System.Drawing;

namespace TagCloudVisualization
{
    public class BasicDrawer : IWordDrawer
    {
        public void DrawWord(Graphics graphics, ImageCreatingOptions options, WordInfo wordInfo, Font font)
        {
            graphics.DrawString(wordInfo.Word, font, options.Brush, wordInfo.Rectangle);
        }

        public bool Check(WordInfo word) => true;

        
    }
}
