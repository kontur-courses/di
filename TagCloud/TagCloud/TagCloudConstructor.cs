using TagsCloudLayouter;

namespace TagCloud;

public class TagCloudConstructor
{
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
        words = new WordPreprocessor().Process(words);
        var wordsFrequency = FrequencyDictionary.GetWordsFrequency(words);
        var textBoxes = wrapper.Wrap(wordsFrequency);
        
        layouter.PlaceTexts(textBoxes);
        layouter.Clear();
        
        return drawer.Draw(textBoxes);
    }

    private ICloudDrawer drawer;
    private IFileLoader textLoader;
    private ApplicationProperties applicationProperties;
    private IWordsParser parser;
    private TextWrapper wrapper;
    private ICloudLayouter layouter;
}