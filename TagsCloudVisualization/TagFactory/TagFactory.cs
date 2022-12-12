using System.Drawing;
using TagsCloudVisualization.CloudLayouter;
using TagsCloudVisualization.ColorGenerator;
using TagsCloudVisualization.Drawer;
using TagsCloudVisualization.FontSettings;

namespace TagsCloudVisualization.TagFactory;

public class TagFactory : ITagFactory
{
    private readonly ICloudLayouter layouter;
    private readonly IColorGenerator colorGenerator;
    private readonly IFontSettingsProvider fontSettingsProvider;

    public TagFactory(ICloudLayouter layouter, IColorGenerator colorGenerator, IFontSettingsProvider fontSettingsProvider)
    {
        this.layouter = layouter;
        this.colorGenerator = colorGenerator;
        this.fontSettingsProvider = fontSettingsProvider;
    }
    public TagImage Create(Tag tag)
    {
        var fontSettings = fontSettingsProvider.GetSettings();
        var height = fontSettings.Size * tag.Weight;
        var size = new SizeF(height * tag.Text.Length, height);
        return new TagImage(tag, layouter.PutNextRectangle(size.ToSize()), fontSettingsProvider, colorGenerator);
    }
}