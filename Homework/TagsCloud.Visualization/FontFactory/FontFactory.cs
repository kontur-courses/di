using TagsCloud.Visualization.Models;

namespace TagsCloud.Visualization.FontFactory
{
    public class FontFactory : IFontFactory
    {
        private readonly FontSettings settings;

        public FontFactory(FontSettings settings) => this.settings = settings;

        public FontDecorator GetFont(Word word, int minCount, int maxCount)
        {
            var (_, count) = word;
            var fontSize = count <= minCount
                ? 1
                : settings.MaxSize * (count - minCount) / (maxCount - minCount);
            return new FontDecorator(settings.FamilyName, fontSize, settings.FontStyle);
        }
    }
}