using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer;

public class FontSizeProvider
{
    private readonly ImageSettings imageSettings;

    public FontSizeProvider(ImageSettings imageSettings)
    {
        this.imageSettings = imageSettings;
    }

    public Size GetSizeByWord(string word)
    {
        using var graphics = Graphics.FromImage(new Bitmap(1, 1));
        var textSize = graphics.MeasureString(word, imageSettings.Font).ToSize();
        return new Size(textSize.Width + 10, textSize.Height + 10);
    }
}