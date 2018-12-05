using System;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class FontSettings
    {
        public FontFamily FontFamily { get; } 
        public FontStyle FontStyle { get; }

        public FontSettings(FontFamily fontFamily, FontStyle fontStyle)
        {
            FontFamily = fontFamily;
            FontStyle = fontStyle;
        }
    }
}