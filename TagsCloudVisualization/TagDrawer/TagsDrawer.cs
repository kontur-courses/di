using System.Drawing;
using TagsCloudVisualization.ImageSavers;
using TagsCloudVisualization.ImageSettings;

namespace TagsCloudVisualization.TagDrawer;

public class TagsDrawer : ITagsDrawer
{
    private readonly IImageSettingsProvider settingsProvider;
    private readonly AbstractImageSaver imageSaver;

    public TagsDrawer(AbstractImageSaver imageSaver, IImageSettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
        this.imageSaver = imageSaver;
    }

    public void Draw(IReadOnlyCollection<TagImage> tagImages, string filePath)
    {
        var settings = settingsProvider.GetSettings();
        using var image = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height);
        using var graphics = Graphics.FromImage(image);
        graphics.Clear(settings.BackgroundColor);

        var bounds = new Rectangle(Point.Empty, settings.ImageSize);
        var offset = settings.ImageSize / 2;

        foreach (var tag in tagImages)
        {
            tag.Bound.Offset(offset.Width, offset.Height);
        }

        Validate(tagImages, bounds);

        DrawTags(tagImages, graphics);

        imageSaver.Save(filePath, image);
    }

    private void DrawTags(IReadOnlyCollection<TagImage> tagImages, Graphics graphics)
    {
        foreach (var drawable in tagImages)
        {
            drawable.Draw(graphics);
        }
    }

    private void Validate(IReadOnlyCollection<TagImage> tagImages, Rectangle bounds)
    {
        foreach (var tag in tagImages)
        {
            if (!bounds.Contains(tag.Bound))
                throw new Exception("Rectangles don't fit");
        }
    }
}