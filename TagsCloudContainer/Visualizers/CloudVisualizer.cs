using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Visualizers;

public class CloudVisualizer: ICloudVisualizer
{
    private readonly IImageSettings settings;

    public CloudVisualizer(IImageSettings settings)
    {
        this.settings = settings;
    }

    public Image DrawImage(ITagCloud cloud)
    {
        var image = new Image<Rgba64>(settings.ImageSize.Width, settings.ImageSize.Height);
        DrawBackground(image);
        foreach (var tag in cloud.Tags)
        {
            DrawTag(image, tag);
        }

        return image;
    }

    private void DrawTag(Image image, Tag tag)
    {
        var location = new PointF(tag.Rectangle.Location.X, tag.Rectangle.Location.Y);
        image.Mutate(ctx => { ctx.DrawText(tag.Word, settings.TextOptions.Font, settings.PrimaryColor, location); });
    }

    private void DrawBackground(Image image)
    {
        image.Mutate(ctx => { ctx.Fill(settings.BackgroundColor); });
    }
}