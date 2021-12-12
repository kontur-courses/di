using System.Drawing;

namespace TagsCloudContainer.Settings
{
    public class FontFamilySettings : IFontFamilySettings
    {
        public FontFamily FontFamily { get; }

        public FontFamilySettings(IRenderSettings settings)
        {
            FontFamily = settings.FontFamily;
        }
    }
}