using TagsCloud.CloudLayouter;
using TagsCloud.Infrastructure;
using TagsCloud.Settings;

namespace TagsCloud.CloudPainter;

public class TagCloudPainter
{
    
    private readonly IImageHolder imageHolder;
    private readonly TagSettings tagSettings;
    private readonly WordAnalyzer.WordAnalyzer wordAnalyzer;
    private readonly FileReader fileReader;

    public TagCloudPainter(IImageHolder imageHolder, TagSettings tagSettings, WordAnalyzer.WordAnalyzer wordAnalyzer,
        FileReader reader)
    {
        this.imageHolder = imageHolder;
        this.tagSettings = tagSettings;
        this.wordAnalyzer = wordAnalyzer;
        fileReader = reader;
    }
    public void Paint(ISpiral spiral, string path)
    {
        using var graphics = imageHolder.StartDrawing();
        var layout = new CloudLayouter.CloudLayouter(spiral);
        var wordList = fileReader.GetWords(path);
        var parsedList = wordAnalyzer.GetFrequencyList(wordList);
        new TagCloudVisualizer(graphics, tagSettings, layout).RenderCloudImage(parsedList, imageHolder.GetImageSize());
        imageHolder.UpdateUi();
    }
}