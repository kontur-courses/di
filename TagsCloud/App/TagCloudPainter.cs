using TagsCloud.App.Settings;
using TagsCloud.CloudLayouter;
using TagsCloud.CloudVisualizer;
using TagsCloud.Infrastructure;

namespace TagsCloud.App;

public class TagCloudPainter
{
    private readonly FileReader fileReader;

    private readonly IImageHolder imageHolder;
    private readonly TagSettings tagSettings;
    private readonly WordAnalyzer.WordAnalyzer wordAnalyzer;

    public TagCloudPainter(IImageHolder imageHolder, TagSettings tagSettings, WordAnalyzer.WordAnalyzer wordAnalyzer,
        FileReader reader)
    {
        this.imageHolder = imageHolder;
        this.wordAnalyzer = wordAnalyzer;
        fileReader = reader;
        this.tagSettings = tagSettings;
    }

    public void Paint(string filePath, ISpiral spiral)
    {
        var parsedWords = wordAnalyzer.GetFrequencyList(fileReader.GetWords(filePath));
        using var graphic = imageHolder.StartDrawing();
        {
            var painter = new TagCloudVisualizer(tagSettings, graphic, imageHolder.GetImageSize());
            painter.DrawTagCloud(spiral, parsedWords);
            imageHolder.UpdateUi();
        }
    }
}