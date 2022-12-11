using System.Drawing;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.ImageSettings;

namespace TagsCloudVisualization.Drawer;

public class ImageDrawer : IDrawer
{
    private readonly IImageSettingsProvider settingsProvider;
    private readonly AbstractImageSaver imageSaver;

    public ImageDrawer(AbstractImageSaver imageSaver, IImageSettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
        this.imageSaver = imageSaver;
    }

    public void Draw(IReadOnlyCollection<IDrawImage> drawImages, string filePath)
    {
        var settings = settingsProvider.GetSettings();
        using var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
        using var graphics = Graphics.FromImage(image);
        graphics.Clear(settings.BackgroundColor);

        var bounds = new Rectangle(Point.Empty, settings.ImageSize);
        var offset = settings.ImageSize / 2;

        var shifted = drawImages.Select(tag => tag.Offset(offset)).ToList();

        Validate(shifted, bounds);

        Draw(shifted, graphics);

        imageSaver.Save(filePath, image);
    }

    private void Draw(IReadOnlyCollection<IDrawImage> drawImages, Graphics graphics)
    {
        foreach (var drawable in drawImages)
        {
            drawable.Draw(graphics);
        }
    }

    private void Validate(IReadOnlyCollection<IDrawImage> tagImages, Rectangle bounds)
    {
        foreach (var tag in tagImages)
        {
            if (!bounds.Contains(tag.Bounds))
                throw new Exception("Rectangles don't fit");
        }
    }
}