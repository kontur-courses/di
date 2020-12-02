using System.Drawing;
using System.Linq;
using TagsCloud.App;
using TagsCloud.Infrastructure;

namespace TagsCloud.App
{
    public class TagscloudSettings
    {
        public Palette Palette { get; set; }
        public ImageSize ImageSize { get; set; }
        public PossibleFonts FontSettings { get; set; }
        public FontFamily CurrentFontFamily { get; set; }
        public FontStyle CurrentFontStyle { get; set; }

        public TagscloudSettings(Palette palette, ImageSize imageSize, PossibleFonts possibleFonts)
        {
            Palette = palette;
            ImageSize = imageSize;
            FontSettings = possibleFonts;
            CurrentFontFamily = possibleFonts.FontFamilies.First();
            CurrentFontStyle = possibleFonts.FontStyles.First();
        }
    }
}
