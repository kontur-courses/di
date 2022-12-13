using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer.Gui;

public class GuiGraphicsProvider : IGraphicsProvider
{
    private readonly IImageListProvider imageListProvider;
    private readonly GuiGraphicsProviderSettings settings;
    private Bitmap? bitmapImage;
    private Graphics? graphics;

    public GuiGraphicsProvider(
        IImageListProvider imageListProvider,
        GuiGraphicsProviderSettings settings)
    {
        this.imageListProvider = imageListProvider;
        this.settings = settings;
    }

    public Graphics Create()
    {
        bitmapImage = new(settings.Width, settings.Height);
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

            var imageBits = cache.ToArray();
            if (settings.Save)
            {
                var randomFileName = $"{Path.GetRandomFileName()}.png";
                var fullPath = Path.Combine(settings.SavePath, randomFileName);
                if (!Directory.Exists(settings.SavePath))
                    Directory.CreateDirectory(settings.SavePath);
                File.WriteAllBytes(fullPath, imageBits);
            }

            imageListProvider.AddImageBits(imageBits);
        }

        graphics = null;
        bitmapImage = null;
    }
}