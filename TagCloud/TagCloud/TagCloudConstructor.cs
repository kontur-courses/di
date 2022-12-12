using TagsCloudLayouter;

namespace TagCloud;

public class TagCloudConstructor
{
    private readonly ApplicationProperties applicationProperties;
    private readonly ICloudDrawer drawer;
    private readonly FrequencyDictionary frequencyDictionary;
    private readonly ICloudLayouter layouter;
    private readonly IWordsParser parser;
    private readonly SizeByFrequency sizeByFrequency;
    private readonly IFileLoader textLoader;
    private readonly IWordPreprocessor wordPreprocessor;

    public TagCloudConstructor(
        ICloudDrawer drawer,
        IFileLoader textLoader,
        ApplicationProperties applicationProperties,
        IWordsParser parser,
        SizeByFrequency sizeByFrequency,
        ICloudLayouter layouter,
        IWordPreprocessor wordPreprocessor,
        FrequencyDictionary frequencyDictionary)
    {
        this.drawer = drawer;
        this.textLoader = textLoader;
        this.applicationProperties = applicationProperties;
        this.parser = parser;
        this.sizeByFrequency = sizeByFrequency;
        this.layouter = layouter;
        this.wordPreprocessor = wordPreprocessor;
        this.frequencyDictionary = frequencyDictionary;
    }

    public Bitmap Construct()
    {
        var text = textLoader.Load(applicationProperties.Path);
        var words = parser.Parse(text);
        var processedWords = wordPreprocessor.Process(words, applicationProperties.CloudProperties.ExcludedWords);
        var wordsFrequency = frequencyDictionary.GetWordsFrequency(processedWords);
        var texts = sizeByFrequency.ResizeAll(wordsFrequency).ToList();
        layouter.PlaceTexts(texts);
        return drawer.Draw(texts);
    }
}