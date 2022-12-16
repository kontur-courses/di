using System.Drawing;
using TagCloudPainter.Coloring;
using TagCloudPainter.Common;

namespace TagCloudPainter.Painters;

public class CloudPainter : ICloudPainter
{
    private readonly IWordColoring _coloring;

    public CloudPainter(IImageSettingsProvider imageSettingsProvider, IWordColoring coloring)
    {
        ImageSettings = imageSettingsProvider.ImageSettings;
        _coloring = coloring;
    }

    private ImageSettings ImageSettings { get; }

    public Bitmap PaintTagCloud(IEnumerable<Tag> tags)
    {
        var btm = new Bitmap(ImageSettings.Size.Width, ImageSettings.Size.Height);
        var g = Graphics.FromImage(btm);
        g.Clear(ImageSettings.BackgroundColor);
        var format = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        foreach (var tag in tags)
        {
            var font = tag.Count > 1
                ? new Font(ImageSettings.Font.FontFamily, ImageSettings.Font.Size + (tag.Count - 1), FontStyle.Bold,
                    GraphicsUnit.Point)
                : ImageSettings.Font;
            g.DrawString(tag.Value, font, new SolidBrush(_coloring.GetColor()), tag.Rectangle, format);
        }

        return btm;
    }
}