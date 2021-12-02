using System.Drawing;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Visualizer : IVisualizer
{
    private readonly IBitmapSettingsProvider settingsProvider;

    public Visualizer(IBitmapSettingsProvider settingsProvider)
    {
        this.settingsProvider = settingsProvider;
    }

    public Bitmap GetBitmap(ITagPacker tags, ILayouter layouter, IStyler styler)
    {
        var bitmap = settingsProvider.CreateEmptyBitmap();
        using (var bitmapGraphics = Graphics.FromImage(bitmap))
        {
            settingsProvider.Apply(bitmapGraphics);
            bitmapGraphics.Clear(Color.Black);
            foreach (var tag in tags.GetTags().Select(styler.Style))
            {
                var size = tag.GetTrueGraphicSize(bitmapGraphics);
                var rectangle = layouter.PutNextRectangle(size);
                tag.DrawSelf(bitmapGraphics, rectangle);
            }
        }

        return bitmap;
    }
}
