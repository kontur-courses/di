using System.Drawing;

namespace TagsCloudContainer.Visualization
{
    public static class StringMeasurer
    {
        private static readonly Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));

        public static SizeF MeasureString(string str, int fontSize)
        {
            return Graphics.MeasureString(str, new Font(FontFamily.GenericMonospace, fontSize));
        }
    }
}