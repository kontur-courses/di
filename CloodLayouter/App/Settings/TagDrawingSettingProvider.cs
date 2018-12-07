using System.Drawing;
using CloodLayouter.Infrastructer;

namespace CloodLayouter.App
{
    public class TagDrawingSettingProvider : ITagDrawingSettingsProvider
    {
        public FontStyle FontStyle { get; set; } = FontStyle.Italic;
        public FontFamily FontFamily { get; set; } = FontFamily.GenericMonospace;
        public float MaxFontSize { get; set; } = 32f;
        public float MinFontSize { get; set; } = 16f;
    }
}