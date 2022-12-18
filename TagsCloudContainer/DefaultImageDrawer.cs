using System.Drawing;
using TagsCloudContainer.Colorers;

namespace TagsCloudContainer;

public class DefaultImageDrawer : IImageDrawer
{
    private readonly IColorProvider colorProvider;
    private readonly Dictionary<string, Rectangle> rectanglesDistribution;
    private readonly Settings settings;
    private readonly Dictionary<string, int> wordsDistribution;

    public DefaultImageDrawer(IRectanglesDistributor rectanglesDistributor, IWordsHandler wordsHandler,
        ISettingsProvider settingProvider, IColorProvider colorProvider)
    {
        settings = settingProvider.Settings;
        if (rectanglesDistributor.DistributedRectangles.Successful) rectanglesDistribution = rectanglesDistributor.DistributedRectangles.Value;
        else DrawnBitmap = new Result<Bitmap>(wordsHandler.WordDistribution.Exception);
        if (wordsHandler.WordDistribution.Successful) wordsDistribution = wordsHandler.WordDistribution.Value;
        else DrawnBitmap = new Result<Bitmap>(wordsHandler.WordDistribution.Exception);
        this.colorProvider = colorProvider;
    }

    public Result<Bitmap> DrawnBitmap { get; private set; }

    public Result<Bitmap> DrawImage()
    {
        if (DrawnBitmap != null) return DrawnBitmap;

        try
        {
            var bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
            var offset = new Point(settings.ImageSize.Width / 2, settings.ImageSize.Height / 2);
            var graphics = Graphics.FromImage(bitmap);
            graphics.FillRectangle(new SolidBrush(settings.BackgroundColor), 0, 0, bitmap.Width, bitmap.Height);

            foreach (var pair in rectanglesDistribution)
            {
                var rect = pair.Value;
                var font = settings.Font;
                var freq = wordsDistribution[pair.Key];
                var sizeAdd = settings.FrequencyGrowth * (wordsDistribution[pair.Key] - 1);
                rect.Offset(offset);
                font = new Font(font.FontFamily, font.Size + sizeAdd, font.Style);
                graphics.DrawString(pair.Key, font, new SolidBrush(colorProvider.ProvideColorForWord(pair.Key, freq)),
                    rect);
            }

            DrawnBitmap = new Result<Bitmap>(bitmap);
        }
        catch (Exception e)
        {
            return new Result<Bitmap>(e);
        }

        return DrawnBitmap;
    }
}