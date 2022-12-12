using System.Drawing;
using System.Drawing.Drawing2D;
using TagCloudPainter.Common;
using TagCloudPainter.Interfaces;

namespace TagCloudPainter;

public class CloudPainter : ICloudPainter
{
    public CloudPainter(IImageSettingsProvider imageSettingsProvider)
    {
        ImageSettings = imageSettingsProvider.ImageSettings;
    }

    private ImageSettings ImageSettings { get; }

    public Bitmap PaintTagCloud(IEnumerable<Tag> tags)
    {
        var btm = new Bitmap(ImageSettings.Size.Width, ImageSettings.Size.Height);
        var g = Graphics.FromImage(btm);
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.PixelOffsetMode = PixelOffsetMode.HighQuality;
        g.Clear(ImageSettings.Palette.BackgroundColor);
        var format = new StringFormat
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Center
        };

        foreach (var tag in tags)
        {
            var font = tag.Count > 1
                ? new Font(ImageSettings.Font.FontFamily, ImageSettings.Font.Size + (tag.Count - 1))
                : ImageSettings.Font;
            g.DrawString(tag.Value, font, new SolidBrush(ImageSettings.Palette.TagsColor), tag.Rectangle, format);
        }

        return btm;
    }
}