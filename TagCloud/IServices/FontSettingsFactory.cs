using TagCloud.Models;

namespace TagCloud.IServices
{
    public class FontSettingsFactory : IFontSettingsFactory
    {
        public FontSettings CreateFontSettings(string fontName)
        {
            return new FontSettings(fontName);
        }
    }
}