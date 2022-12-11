using System.Drawing;
using TagsCloudContainer.Colorers;

namespace TagsCloudContainer;

public class DefaultImageDrawer : IImageDrawer
{
    private readonly IColorer colorer;
    private readonly IRectanglesDistributor rectanglesDistributor;
    private readonly Settings settings;
    private readonly IWordsHandler wordsHandler;

    public DefaultImageDrawer(IRectanglesDistributor rectanglesDistributor, IWordsHandler wordsHandler,
        ISettingsProvider settingProvider, IColorer colorer)
    {
        settings = settingProvider.Settings;
        this.rectanglesDistributor = rectanglesDistributor;
        this.wordsHandler = wordsHandler;
        this.colorer = colorer;
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
            var freq = wordsHandler.WordDistribution[pair.Key];
            var ratio = MathF.Pow(settings.FrequencyRatio, freq - 1);
            rect.Offset(offset);
            font = new Font(font.FontFamily, font.Size * ratio, font.Style);
            graphics.DrawString(pair.Key, font, new SolidBrush(colorer.ProvideColorForWord(pair.Key, freq)),
                rect);
        }

        return DrawnBitmap;
    }
}