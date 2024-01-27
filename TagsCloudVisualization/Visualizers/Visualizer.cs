using System.Drawing;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.Visualizers;

public class Visualizer : IVisualizer
{
    private readonly ImageSettings imageSettings;
    private readonly BackgroundSettings backgroundSettings;
    private readonly IColorGenerator colorGenerator;

    public Visualizer(ImageSettings imageSettings, BackgroundSettings backgroundSettings, IColorGeneratorFactory factory)
    {
        this.imageSettings = imageSettings;
        colorGenerator = factory.Create();
        this.backgroundSettings = backgroundSettings;
    }

    public Bitmap Vizualize(IEnumerable<Tag> tags)
    {
        var bitmap = new Bitmap(imageSettings.Width, imageSettings.Height);
        var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(backgroundSettings.BackgroundColor);

        foreach (var tag in tags)
        {
            graphics.DrawString(tag.Content, 
                new Font(tag.Font, tag.Size), 
                new SolidBrush(colorGenerator.GetColor()), 
                tag.Rectangle);
        }
        return bitmap;
    }
}