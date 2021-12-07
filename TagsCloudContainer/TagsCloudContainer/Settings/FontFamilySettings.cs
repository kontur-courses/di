using System.Drawing;
using TagsCloudContainer.Settings.Interfaces;

namespace TagsCloudContainer.Settings
{
    public class FontFamilySettings : IFontFamilySettings
    {
        public FontFamily FontFamily { get; }

        public FontFamilySettings(FontFamily fontFamily)
        {
            FontFamily = fontFamily;
        }
    }
}