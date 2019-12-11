using System.Drawing;

namespace TagCloud.Models
{
    public class FontSettings
    {
        public readonly Color color;
        public readonly float defaultFontSize;
        public readonly FontFamily fontFamily;
        public readonly FontStyle fontStyle;

        public FontSettings()
        {
            defaultFontSize = 12;
            fontFamily = new FontFamily("Arial");
            fontStyle = FontStyle.Bold;
            color = Color.Black;
        }

        public FontSettings(string fontName)
        {
            defaultFontSize = 12;
            fontFamily = new FontFamily(fontName);
            fontStyle = FontStyle.Bold;
            color = Color.Black;
        }
    }
}