using System.Drawing;
using TagsCloudApp.RenderCommand;
using TagsCloudContainer.Settings;

namespace TagsCloudApp.Settings
{
    public class FontFamilySettings : IFontFamilySettings
    {
        public FontFamily FontFamily { get; }

        public FontFamilySettings(IRenderOptions renderOptions)
        {
            FontFamily = renderOptions.FontFamily;
        }
    }
}