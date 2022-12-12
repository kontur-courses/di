using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Cli;

public class CliGraphicsProvider : IGraphicsProvider
{
    private readonly CliGraphicsProviderSettings cliGraphicsProviderSettings;
    private Bitmap? bitmapImage;
    private Graphics? graphics;

    public CliGraphicsProvider(CliGraphicsProviderSettings cliGraphicsProviderSettings)
    {
        this.cliGraphicsProviderSettings = cliGraphicsProviderSettings;
    }

    public Graphics Create()
    {
        Commit();
        bitmapImage = new(cliGraphicsProviderSettings.Width, cliGraphicsProviderSettings.Height);
        graphics = Graphics.FromImage(bitmapImage);
        return graphics;
    }

    public void Commit()
    {
        graphics?.Dispose();
        if (bitmapImage is not null)
        {
            using var cache = new MemoryStream();
            bitmapImage.Save(cache, ImageFormat.Png);
            bitmapImage.Dispose();

            var randomFileName = $"{Path.GetRandomFileName()}.png";
            var fullPath = Path.Combine(cliGraphicsProviderSettings.BasePath, randomFileName);
            if (!Directory.Exists(cliGraphicsProviderSettings.BasePath))
                Directory.CreateDirectory(cliGraphicsProviderSettings.BasePath);
            File.WriteAllBytes(fullPath, cache.ToArray());
        }

        graphics = null;
        bitmapImage = null;
    }
}