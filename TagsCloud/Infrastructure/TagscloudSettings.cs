using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloud.Infrastructure
{
    public class TagsCloudSettings
    {
        public TagsCloudSettings(Palette palette, ImageSize imageSize, PossibleFonts possibleFonts,
            double cloudToImageScaleRatio)
        {
            Palette = palette;
            CloudToImageScaleRatio = cloudToImageScaleRatio;
            ImageSize = imageSize;
            FontSettings = possibleFonts;
            CurrentFontFamily = possibleFonts.FontFamilies.First();
            CurrentFontStyle = possibleFonts.FontStyles.First();
        }

        public Palette Palette { get; set; }
        public ImageSize ImageSize { get; set; }
        public PossibleFonts FontSettings { get; set; }
        public FontFamily CurrentFontFamily { get; set; }
        public FontStyle CurrentFontStyle { get; set; }
        public double CloudToImageScaleRatio { get; set; }

        public static TagsCloudSettings DefaultSettings => new TagsCloudSettings(new Palette(Color.Aqua, Color.Black),
            new ImageSize(500, 500),
            new PossibleFonts(new HashSet<FontStyle> {FontStyle.Regular, FontStyle.Italic, FontStyle.Bold},
                FontFamily.Families.ToHashSet()), 0.7);
    }
}