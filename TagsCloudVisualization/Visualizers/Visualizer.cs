using System.Drawing;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.Visualizers;

public class Visualizer : IVisualizer
{
    private readonly FontSettings fontSettings;
    private readonly ImageSettings imageSettings;
    private readonly IColorGenerator colorGenerator;

    public Visualizer(FontSettings fontSettings, ImageSettings imageSettings, IColorGenerator colorGenerator)
    {
        this.fontSettings = fontSettings;
        this.imageSettings = imageSettings;
        this.colorGenerator = colorGenerator;
    }

    public Bitmap Vizualize(IEnumerable<Tag> tags)
    {
        var bitmap = new Bitmap(imageSettings.Width, imageSettings.Height);
        var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(imageSettings.BackgroundColor);

        foreach (var tag in tags)
        {
            graphics.DrawString(tag.Content, 
                new Font(fontSettings.FontFamily, tag.Size), 
                new SolidBrush(colorGenerator.GetColor()), 
                tag.Rectangle);
        }
        return bitmap;
    }
}