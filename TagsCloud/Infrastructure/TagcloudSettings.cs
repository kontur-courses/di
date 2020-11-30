using System.Drawing;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagcloudSettings
    {
        public Palette Palette { get; set; }
        public ImageSize ImageSize { get; set; }
        public Font WordsFont { get; set; }

        public TagcloudSettings(Palette palette, ImageSize imageSize, Font wordsFont)
        {
            this.Palette = palette;
            ImageSize = imageSize;
            WordsFont = wordsFont;
        }
    }
}
