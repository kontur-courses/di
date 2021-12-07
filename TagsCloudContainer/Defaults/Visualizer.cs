using Autofac;
using System.Drawing;
using TagsCloudContainer.Defaults.SettingsProviders;
using TagsCloudVisualization.Abstractions;

namespace TagsCloudContainer.Defaults;

public class Visualizer : IVisualizer
{
    private readonly BitmapSetting settingsProvider;
    private readonly ITagPacker tags;
    private readonly ILayouter layouter;
    private readonly IStyler styler;

    public Visualizer(BitmapSetting settingsProvider, ITagPacker tags, ILayouter layouter, IStyler styler)
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
            foreach (var tag in tags.GetTags().Select(styler.Style))
            {
                var size = tag.GetTrueGraphicSize(bitmapGraphics);
                var rectangle = layouter.PutNextRectangle(size);
                tag.DrawSelf(bitmapGraphics, rectangle);
            }
        }

        return bitmap;
    }

    [Register]
    public static void Register(ContainerBuilder builder)
    {
        builder.RegisterType<Visualizer>().AsSelf().As<IVisualizer>();
    }
}
