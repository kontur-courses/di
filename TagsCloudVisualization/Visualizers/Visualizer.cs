using System.Drawing;
using TagsCloudVisualization.ColorGenerators;
using TagsCloudVisualization.Settings;
using TagsCloudVisualization.Tags;

namespace TagsCloudVisualization.Visualizers;

public class Visualizer : IVisualizer
{
    private readonly ImageSettings imageSettings;
    private readonly BackgroundSettings backgroundSettings;
    private readonly IColorGenerator[] colorGenerators;

    public Visualizer(ImageSettings imageSettings, BackgroundSettings backgroundSettings, IColorGenerator[] generators)
    {
        this.imageSettings = imageSettings;
        colorGenerators = generators;
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
                new SolidBrush(GetColorIfMatch()), 
                tag.Rectangle);
        }
        return bitmap;
    }

    public Color GetColorIfMatch()
    {
        var generator = colorGenerators.Where(g => g.Match()).FirstOrDefault();
        return generator is null 
            ? throw new ArgumentException("Can't find color") 
            : generator.GetColor();
    }
}