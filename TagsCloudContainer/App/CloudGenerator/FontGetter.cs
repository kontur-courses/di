using System.Drawing;
using TagsCloudContainer.Infrastructure.CloudGenerator;

namespace TagsCloudContainer.App.CloudGenerator
{
    internal class FontGetter : IFontGetter
    {
        private readonly int defaultFontSize = 10;
        private readonly string fontName;

        public FontGetter(string fontName)
        {
            this.fontName = fontName;
        }

        public Font GetFont(string word, double frequency)
        { 
            var fontSize = defaultFontSize * (1 + frequency * defaultFontSize);
            return new Font(fontName, (float) fontSize);
        }
    }
}