using TagCloud.Abstractions;

namespace ConsoleClient;

public class Client
{
    private readonly ICloudCreator creator;
    private readonly IWordsLoader loader;
    private readonly IEnumerable<IWordsProcessor> processors;
    private readonly IWordsTagger tagger;

    public Client(
        IWordsLoader loader,
        IEnumerable<IWordsProcessor> processors,
        IWordsTagger tagger,
        ICloudCreator creator)
    {
        this.loader = loader;
        this.processors = processors;
        this.tagger = tagger;
        this.creator = creator;
    }

    public void Execute(string resultFilepath)
    {
        var words = loader.Load();
        words = processors.Aggregate(words, (current, processor) => processor.Process(current));
        var tags = tagger.ToTags(words);
        var bitmap = creator.CreateTagCloud(tags);
        bitmap.Save(resultFilepath);
    }
}