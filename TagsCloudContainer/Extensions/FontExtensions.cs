using System.Drawing;

namespace TagsCloudContainer.Extensions
{
    public static class FontExtensions
    {
        public static Font SetSize(this Font font, float emSize)
        {
            return new Font(font.FontFamily, emSize, font.Style);
        }
    }
}