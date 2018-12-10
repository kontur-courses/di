using System.Drawing;
using TagsCloudContainer.FontSizesChoosers;
using TagsCloudContainer.ImageCreators;

namespace TagsCloudContainer.Settings
{
    public class TextSettings
    {
        public FontFamily Family;
        public IColorChooser ColorChooser;
        public IFontSizeChooser FontSizeChooser;

        public TextSettings(FontFamily fontFamily, IColorChooser colorChooser, IFontSizeChooser fontSizeChooser)
        {
            Family = fontFamily;
            ColorChooser = colorChooser;
            FontSizeChooser = fontSizeChooser;
        }
    }
}