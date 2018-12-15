using System.Drawing;
using TagsCloudContainer.FontSizesChoosers;
using TagsCloudContainer.ImageCreators;

namespace TagsCloudContainer.Settings
{
    public class TextSettings : ITextSettings
    {
        public FontFamily Family { get; set; }
        public IColorChooser ColorChooser { get; set; }
        public IFontSizeChooser FontSizeChooser { get; set; }
        public string DictionaryLocale { get; set; }


        public TextSettings(IColorChooser colorChooser, IFontSizeChooser fontSizeChooser)
        {
            Family = new FontFamily("Arial");
            ColorChooser = colorChooser;
            FontSizeChooser = fontSizeChooser;
            DictionaryLocale = "en_US";
        }
    }
}