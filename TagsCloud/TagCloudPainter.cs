using Castle.Core.Internal;
using DeepMorphy;
using TagsCloud.Infrastructure;
using TagsCloud.Settings;

namespace TagsCloud;

public class TagCloudPainter
{
    
    private readonly IImageHolder imageHolder;
    private readonly TagSettings tagSettings;
    private readonly WordAnalyzerSettings wordAnalyzerSettings;
    private readonly FileReader fileReader;

    public TagCloudPainter(IImageHolder imageHolder, TagSettings tagSettings, WordAnalyzerSettings wordAnalyzerSettings,
        FileReader reader)
    {
        this.imageHolder = imageHolder;
        this.tagSettings = tagSettings;
        this.wordAnalyzerSettings = wordAnalyzerSettings;
        fileReader = reader;
    }

    public void Paint(ISpiral spiral, string path)
    {
        using (var graphics = imageHolder.StartDrawing())
        {
            var wordAnalyzer = new WordAnalyzer(wordAnalyzerSettings);
            var layout = new CloudLayouter(spiral);
            var wordList = fileReader.GetWords(path);
            var parsedList = wordAnalyzer.GetFrequencyList(wordList);
            var tagGenerator = new TagGenerator(layout, tagSettings);
            RectanglesVisualizer.RenderCloudImage(tagGenerator.GetTagsList(parsedList).ToList(), graphics, imageHolder);
            imageHolder.UpdateUi();
        }
        imageHolder.UpdateUi();
    }
}