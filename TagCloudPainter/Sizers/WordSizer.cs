using System.Drawing;
using TagCloudPainter.Common;

namespace TagCloudPainter.Sizers;

public class WordSizer : IWordSizer
{
    private readonly ImageSettings settings;

    public WordSizer(IImageSettingsProvider provider)
    {
        settings = provider.ImageSettings;
    }

    public Size GetTagSize(string word, int count)
    {
        if (String.IsNullOrWhiteSpace(word) || count < 1)
            throw new ArgumentNullException();

        var width = word.Length * (int)(settings.Font.Size + 1) + (3 * (count-1));
        var height = (settings.Size.Height + settings.Size.Width) / 40 + (2 * (count - 1));
        return new Size(width, height);
    }
}