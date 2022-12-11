using TagsCloudVisualization.Drawer;
using TagsCloudVisualization.Preprocessors;
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
    private readonly IDrawer drawer;

    public Visualizer(ITextProvider textProvider,
        IPreprocessor preprocessor,
        IToTagConverter tagConverter,
        ITagFactory tagFactory,
        IDrawer drawer)
    {
        this.textProvider = textProvider;
        this.preprocessor = preprocessor;
        this.tagConverter = tagConverter;
        this.tagFactory = tagFactory;
        this.drawer = drawer;
    }

    public void Visualize(string path, int tagCount)
    {
        var text = textProvider.GetText();
        var processedText = preprocessor.Process(text);
        var tags = tagConverter.Convert(processedText);
        var tagImages = tags.Select(x => tagFactory.Create(x)).Take(tagCount).ToList();
        drawer.Draw(tagImages,path);
    }
    
}