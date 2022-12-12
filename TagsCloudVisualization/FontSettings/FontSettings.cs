using System;
using System.Drawing;

namespace TagsCloudVisualization.FontSettings
{
    internal class FontSettings : IFontSettings
    {
        public FontFamily FontFamily { get; set; }
        public string FontColor { get; set; }

        private readonly string defaultFontFamily = "Arial";

        public FontSettings(string fontFamily, string fontColor)
        {
            FontFamily = TryGetFontFamily(fontFamily);
            FontColor = fontColor;
        }

        private FontFamily TryGetFontFamily(string fontFamily)
        {
            try
            {
                return new FontFamily(fontFamily);
            }
            catch (ArgumentException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Font {fontFamily} cannot be found. The default value has been set: {defaultFontFamily}");
                Console.ResetColor();
                return new FontFamily(defaultFontFamily);
            }
        }
    }
}
