using System.Drawing;
using TagsCloudContainer.App.Settings;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class FontGetter : IFontGetter
    {
        private readonly AppSettings appSettings;

        public FontGetter(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public Font GetFont(string word, double frequency)
        {
            var font = appSettings.FontSettings.Font;
            var fontSize = font.Size * (1 + frequency * font.Size);
            return new Font(font.FontFamily,
                (float) fontSize, font.Style,
                font.Unit, font.GdiCharSet);
        }
    }
}