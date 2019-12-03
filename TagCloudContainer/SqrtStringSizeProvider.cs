using System;
using System.Drawing;
using TagCloudContainer.Api;

namespace TagCloudContainer
{
    public class SqrtStringSizeProvider : IStringSizeProvider
    {
        public static Graphics GraphicsBase = Graphics.FromImage(new Bitmap(1, 1));
        public Font Font;

        public SqrtStringSizeProvider(Font font)
        {
            Font = font;
        }

        public Size GetStringSize(string word, int occurrenceCount)
        {
            return (GraphicsBase.MeasureString(word, Font) * (MathF.Log(occurrenceCount) + 1f)).ToSize();
        }
    }
}