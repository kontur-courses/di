using System.Drawing;

namespace TagsCloudContainer;

public class DefaultImageDrawer : IImageDrawer
{
    public Bitmap DrawnBitmap { get; private set; }

    private Settings settings;
    private IRectanglesDistributor rectanglesDistributor;

    public DefaultImageDrawer(IRectanglesDistributor rectanglesDistributor,
        ISettingsProvider settingProvider)
    {
        settings = settingProvider.Settings;
        this.rectanglesDistributor = rectanglesDistributor;
        DrawImage();
    }

    private void DrawImage()
    {
        DrawnBitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
        var graphics=Graphics.FromImage(DrawnBitmap);
        foreach (var pair in rectanglesDistributor.DistributedRectangles)
        {
            graphics.DrawString(pair.Key, settings.Font, settings.Brush, pair.Value);
        }
    }
}