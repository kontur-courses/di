using TagCloud.Abstractions;

namespace ConsoleClient;

public class Client
{
    private readonly ICloudCreator creator;
    private readonly ICloudDrawer drawer;
    private readonly IWordsLoader loader;
    private readonly IEnumerable<IWordsProcessor> processors;
    private readonly IWordsTagger tagger;

    public Client(
        IWordsLoader loader,
        IEnumerable<IWordsProcessor> processors,
        IWordsTagger tagger,
        ICloudCreator creator,
        ICloudDrawer drawer)
    {
        this.loader = loader;
        this.processors = processors;
        this.tagger = tagger;
        this.creator = creator;
        this.drawer = drawer;
    }

    public void Execute(string resultFilepath)
    {
        var words = loader.Load();
        words = processors.Aggregate(words, (current, processor) => processor.Process(current));
        var tags = tagger.ToTags(words);
        var drawableTags = creator.CreateTagCloud(tags);
        var bitmap = drawer.Draw(drawableTags);
        bitmap.Save(resultFilepath);
    }
}