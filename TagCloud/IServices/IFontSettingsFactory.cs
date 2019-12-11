using TagCloud.Models;

namespace TagCloud.IServices
{
    public interface IFontSettingsFactory
    {
        FontSettings CreateFontSettings(string fontName);
    }
}