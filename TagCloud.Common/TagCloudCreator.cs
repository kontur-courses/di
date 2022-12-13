using TagCloud.Common.Drawing;
using TagCloud.Common.Options;
using TagCloud.Common.TagsConverter;
using TagCloud.Common.TextFilter;

namespace TagCloud.Common;

public class TagCloudCreator
{
    private ICloudDrawer drawer;
    private ITagsConverter converter;
    private ITextFilter filter;

    public TagCloudCreator(ICloudDrawer drawer, ITagsConverter converter, ITextFilter filter)
    {
        this.drawer = drawer;
        this.converter = converter;
        this.filter = filter;
    }

    public void CreateCloud(VisualizationOptions visualizationOptions)
    {
        var lines = File.ReadAllLines(visualizationOptions.PathToTextFile);
        var words = filter.FilterAllWords(lines,
            visualizationOptions.BoringWordsThreshold);
        var tags = converter.ConvertToTags(words, visualizationOptions.MinFontSize);
        drawer.DrawCloud(tags, visualizationOptions);
    }
}