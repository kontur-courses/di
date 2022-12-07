using TagsCloudContainer.Drawing;
using TagsCloudContainer.Layouter;
using TagsCloudContainer.Options;
using TagsCloudContainer.TagsConverter;

namespace TagsCloudContainer;

public class TagCloudCreator
{
    private ICloudDrawer drawer;
    private ITagsConverter converter;

    public TagCloudCreator(ICloudDrawer drawer, ITagsConverter converter)
    {
        this.drawer = drawer;
        this.converter = converter;
    }

    public void CreateCloud(VisualizationOptions visualizationOptions)
    {
        var words = File.ReadAllLines(visualizationOptions.PathToTextFile).Select(word => word.ToLower())
            .Where(line => line.Length > visualizationOptions.BoringWordsThreshold)
            .ToList();
        var tags = converter.ConvertToTags(words, visualizationOptions.MinFontSize);
        drawer.DrawCloud(tags, visualizationOptions);
    }
}