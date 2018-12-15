using System.Drawing;
using TagsCloudContainer.FontSizesChoosers;
using TagsCloudContainer.ImageCreators;

namespace TagsCloudContainer.Settings
{
    public interface ITextSettings
    {
        FontFamily Family { get; set; }
        IColorChooser ColorChooser { get; set; }
        IFontSizeChooser FontSizeChooser { get; set; }
        string DictionaryLocale { get; set; }
    }
}