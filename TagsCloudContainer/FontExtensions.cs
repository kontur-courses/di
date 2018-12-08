using System.Drawing;

namespace TagsCloudContainer
{
    public static class FontExtensions
    {
        public static SizeF GetStringSize(this Font font, string str)
        {
            using (var g = Graphics.FromImage(new Bitmap(1, 1)))
            {
                return g.MeasureString(str, font);
            }
        }
    }
}
