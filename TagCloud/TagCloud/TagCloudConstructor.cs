using TagsCloudLayouter;

namespace TagCloud;

public class TagCloudConstructor
{
    private readonly ApplicationProperties applicationProperties;
    private readonly ICloudDrawer drawer;
    private readonly ICloudLayouter layouter;
    private readonly IWordsParser parser;
    private readonly IFileLoader textLoader;
    private readonly TextWrapper wrapper;

    public TagCloudConstructor(
        ICloudDrawer drawer,
        IFileLoader textLoader,
        ApplicationProperties applicationProperties,
        IWordsParser parser,
        TextWrapper wrapper,
        ICloudLayouter layouter)
    {
        this.drawer = drawer;
        this.textLoader = textLoader;
        this.applicationProperties = applicationProperties;
        this.parser = parser;
        this.wrapper = wrapper;
        this.layouter = layouter;
    }

    public Bitmap Construct()
    {
        var text = textLoader.Load(applicationProperties.Path);
        var words = parser.Parse(text);
        words = new WordPreprocessor().Process(words, applicationProperties.CloudProperties.ExcludedWords);
        var wordsFrequency = FrequencyDictionary.GetWordsFrequency(words);
        var texts = wrapper.Wrap(wordsFrequency);

        layouter.PlaceTexts(texts);
        layouter.Clear();

        return drawer.Draw(texts);
    }
}