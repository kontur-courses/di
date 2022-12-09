using System.Drawing;
using System.Drawing.Imaging;
using TagCloudPainter.Common;
using TagCloudPainter.Interfaces;

namespace TagCloudPainter;

public class CloudPainter : ICloudPainter
{
    private readonly ITagCloudElementsBuilder tagCloudElementsBuilder;

    public CloudPainter(ITagCloudElementsBuilder builder, IImageSettingsProvider imageSettingsProvider)
    {
        tagCloudElementsBuilder = builder;
        ImageSettings = imageSettingsProvider.ImageSettings;
    }

    private ImageSettings ImageSettings { get; }

    public void PaintTagCloud(string inputPath, string outputPath, ImageFormat imageFormat)
    {
        var btm = new Bitmap(ImageSettings.Size.Width, ImageSettings.Size.Height);
        var g = Graphics.FromImage(btm);
        g.Clear(ImageSettings.Palette.BackgroundColor);
        var tags = tagCloudElementsBuilder.GetTags(inputPath);

        foreach (var tag in tags)
            g.DrawString(tag.Value, ImageSettings.Font, new SolidBrush(ImageSettings.Palette.TagsColor), tag.Rectangle);

        btm.Save(outputPath, imageFormat);
    }
}