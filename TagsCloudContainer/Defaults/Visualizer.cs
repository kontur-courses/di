using System.Drawing;
using TagsCloudContainer.Abstractions;
using TagsCloudContainer.Defaults.SettingsProviders;

namespace TagsCloudContainer.Defaults;

public class Visualizer : IVisualizer
{
    private readonly VisualizerSettings settingsProvider;
    private readonly ITagPacker tags;
    private readonly ILayouter layouter;
    private readonly IStyler styler;

    public Visualizer(VisualizerSettings settingsProvider, ITagPacker tags, ILayouter layouter, IStyler styler)
    {
        this.settingsProvider = settingsProvider;
        this.tags = tags;
        this.layouter = layouter;
        this.styler = styler;
    }

    public Bitmap GetBitmap()
    {
        var bitmap = new Bitmap(settingsProvider.Width, settingsProvider.Height);
        using (var bitmapGraphics = Graphics.FromImage(bitmap))
        {
            bitmapGraphics.SmoothingMode = settingsProvider.SmoothingMode;
            bitmapGraphics.Clear(Color.Black);
            var count = 0;
            foreach (var tag in tags.GetTags().Select(styler.Style))
            {
                var size = tag.GetTrueGraphicSize(bitmapGraphics);
                var rectangle = layouter.PutNextRectangle(size);
                tag.DrawSelf(bitmapGraphics, rectangle);
                count++;
                if (settingsProvider.WordLimit != 0 && count >= settingsProvider.WordLimit)
                    break;
            }
        }

        return bitmap;
    }
}
