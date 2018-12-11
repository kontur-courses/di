using System.Drawing;

namespace TagCloud.Core.Util
{
    public static class FontExtensions
    {
        public static Font WithModifiedFontSizeOf(this Font primaryFont, float fontSizeDelta)
        {
            return new Font(primaryFont.Name, primaryFont.Size + fontSizeDelta,
                primaryFont.Style, primaryFont.Unit, primaryFont.GdiCharSet, primaryFont.GdiVerticalFont);
        }
    }
}