using System;
using System.Drawing;
using TagsCloudVisualization.Interfaces;

namespace TagsCloudVisualization
{
    internal class WordFrequencySizeSelector : ISizableSelector<string, int>
    {
        public static Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));
        public Font Font;

        public WordFrequencySizeSelector(Font font)
        {
            Font = font;
        }

        public Size GetSize(string obj, int info)
        {
            return (Graphics.MeasureString(obj, Font) * (MathF.Log(info) + 1)).ToSize();
        }
    }
}