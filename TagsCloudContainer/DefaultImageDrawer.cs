using System.Drawing;
using TagsCloudContainer.Colorers;

namespace TagsCloudContainer;

public class DefaultImageDrawer : IImageDrawer
{
    private readonly IColorProvider _colorProvider;
    private readonly IRectanglesDistributor rectanglesDistributor;
    private readonly Settings settings;
    private readonly IWordsHandler wordsHandler;

    public DefaultImageDrawer(IRectanglesDistributor rectanglesDistributor, IWordsHandler wordsHandler,
        ISettingsProvider settingProvider, IColorProvider colorProvider)
    {
        settings = settingProvider.Settings;
        this.rectanglesDistributor = rectanglesDistributor;
        this.wordsHandler = wordsHandler;
        this._colorProvider = colorProvider;
    }

    public Bitmap DrawnBitmap { get; private set; }

    public Result<Bitmap> DrawImage()
    {
        if (DrawnBitmap != null) return new Result<Bitmap>(DrawnBitmap);

        try
        {
            DrawnBitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var offset = new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2);
            var graphics = Graphics.FromImage(DrawnBitmap);
            graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), 0, 0, DrawnBitmap.Width, DrawnBitmap.Height);

            foreach (var pair in rectanglesDistributor.DistributedRectangles)
            {
                var rect = pair.Value;
                var font = settings.Font;
                var freq = wordsHandler.WordDistribution[pair.Key];
                var sizeAdd = settings.FrequencyGrowth * (wordsHandler.WordDistribution[pair.Key] - 1);
                rect.Offset(offset);
                font = new Font(font.FontFamily, font.Size + sizeAdd, font.Style);
                graphics.DrawString(pair.Key, font, new SolidBrush(_colorProvider.ProvideColorForWord(pair.Key, freq)),
                    rect);
            }
        }
        catch (Exception e)
        {
            return new Result<Bitmap>(e);
        }

        return new Result<Bitmap>(DrawnBitmap);
    }
}