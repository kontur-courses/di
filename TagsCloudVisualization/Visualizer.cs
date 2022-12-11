using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.TagDrawer;
using TagsCloudVisualization.TagFactory;
using TagsCloudVisualization.TextProviders;
using TagsCloudVisualization.ToTagConverter;

namespace TagsCloudVisualization;

public class Visualizer
{
    private readonly ITextProvider textProvider;
    private readonly IPreprocessor preprocessor;
    private readonly IToTagConverter tagConverter;
    private readonly ITagFactory tagFactory;
    private readonly ITagsDrawer tagsDrawer;

    public Visualizer(ITextProvider textProvider,
        IPreprocessor preprocessor,
        IToTagConverter tagConverter,
        ITagFactory tagFactory,
        ITagsDrawer tagsDrawer)
    {
        this.textProvider = textProvider;
        this.preprocessor = preprocessor;
        this.tagConverter = tagConverter;
        this.tagFactory = tagFactory;
        this.tagsDrawer = tagsDrawer;
    }

    public void Visualize(string path, int tagCount)
    {
        var text = textProvider.GetText();
        var processedText = preprocessor.Process(text);
        var tags = tagConverter.Convert(processedText);
        var tagImages = tags.Select(x => tagFactory.Create(x)).Take(tagCount).ToList();
        tagsDrawer.Draw(tagImages,path);
    }
    
}