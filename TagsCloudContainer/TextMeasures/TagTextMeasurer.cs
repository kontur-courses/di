using System.Drawing;
using SixLabors.Fonts;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.TextMeasures;

public class TagTextMeasurer: ITagTextMeasurer
{
    private readonly IImageSettings imageSettings;

    public TagTextMeasurer(IImageSettings imageSettings)
    {
        this.imageSettings = imageSettings;
    }

    public Size Measure(string text)
    {
        var fontRectangle = TextMeasurer.MeasureSize(text, imageSettings.TextOptions);
        return new Size((int)fontRectangle.Width, (int)fontRectangle.Height);
    }
}