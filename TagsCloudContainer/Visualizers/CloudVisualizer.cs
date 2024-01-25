using System.Drawing;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Visualizers;

public class CloudVisualizer : ICloudVisualizer
{
    private readonly ImageSettings imageSettings;
    private readonly ITagsCloudGenerator cloudGenerator;
    private Image image;

    public CloudVisualizer(ImageSettings imageSettings, ITagsCloudGenerator cloudGenerator)
    {
        this.imageSettings = imageSettings;
        this.cloudGenerator = cloudGenerator;
    }

    public Image GenerateImage()
    {
        image = new Bitmap(imageSettings.ImageSize.Width, imageSettings.ImageSize.Height);
        DrawBackground();
        var cloud = cloudGenerator.Generate();
        VisualizeTags(cloud);

        return image;
    }

    public void VisualizeTag(Tag tag)
    {
        if (image is null)
            throw new InvalidOperationException("Изображение не проинициализированно");

        using var g = Graphics.FromImage(image);
        var brush = new SolidBrush(imageSettings.PrimaryColor);
        g.DrawString(tag.Word, imageSettings.Font, brush, tag.Rectangle.Location);
        var pen = new Pen(imageSettings.SecondaryColor, imageSettings.Font.Size / 2);
        g.DrawRectangle(pen, tag.Rectangle);
    }

    public void VisualizeTags(IEnumerable<Tag> tags)
    {
        foreach (var rect in tags)
        {
            VisualizeTag(rect);
        }
    }
    
    public void SaveImage()
    {
        image.Save(imageSettings.File);
    }

    private void DrawBackground()
    {
        using var g = Graphics.FromImage(image);
        var brush = new SolidBrush(imageSettings.BackgroundColor);
        g.FillRectangle(brush, 0, 0, image.Width, image.Height);
    }
}