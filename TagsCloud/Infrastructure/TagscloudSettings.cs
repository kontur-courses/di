using System.Drawing;
using System.Linq;

namespace TagsCloud.Infrastructure
{
    public class TagsCloudSettings
    {
        public Palette Palette { get; set; }
        public ImageSize ImageSize { get; set; }
        public PossibleFonts FontSettings { get; set; }
        public FontFamily CurrentFontFamily { get; set; }
        public FontStyle CurrentFontStyle { get; set; }
        public double CloudToImageScaleRatio { get; set; }

        public TagsCloudSettings(Palette palette, ImageSize imageSize, PossibleFonts possibleFonts, double cloudToImageScaleRatio)
        {
            Palette = palette;
            this.CloudToImageScaleRatio = cloudToImageScaleRatio;
            ImageSize = imageSize;
            FontSettings = possibleFonts;
            CurrentFontFamily = possibleFonts.FontFamilies.First();
            CurrentFontStyle = possibleFonts.FontStyles.First();
        }
    }
}
