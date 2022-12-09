using System.Drawing;

namespace TagsCloudContainer;

public class DefaultImageDrawer : IImageDrawer
{
    private readonly IRectanglesDistributor rectanglesDistributor;

    private readonly Settings settings;
    private readonly IWordsHandler wordsHandler;

    public DefaultImageDrawer(IRectanglesDistributor rectanglesDistributor, IWordsHandler wordsHandler,
        ISettingsProvider settingProvider)
    {
        settings = settingProvider.Settings;
        this.rectanglesDistributor = rectanglesDistributor;
        this.wordsHandler = wordsHandler;
    }

    public Bitmap DrawnBitmap { get; private set; }

    public Bitmap DrawImage()
    {
        if (DrawnBitmap != null) return DrawnBitmap;

        DrawnBitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
        var offset = new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2);
        var graphics = Graphics.FromImage(DrawnBitmap);
        graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), 0, 0, DrawnBitmap.Width, DrawnBitmap.Height);

        foreach (var pair in rectanglesDistributor.DistributedRectangles)
        {
            var rect = pair.Value;
            var font = settings.Font;
            var ratio = MathF.Pow(settings.FrequencyRatio, wordsHandler.WordDistribution[pair.Key] - 1);
            rect.Offset(offset);
            font = new Font(font.FontFamily, font.Size * ratio, font.Style);
            graphics.DrawString(pair.Key, font, new SolidBrush(settings.FontColor), rect);
        }

        return DrawnBitmap;
    }
}