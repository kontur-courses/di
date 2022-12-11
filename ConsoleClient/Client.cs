using TagCloud.Abstractions;

namespace ConsoleClient;

public class Client
{
    private readonly ICloudCreator creator;
    private readonly IWordsLoader wordsLoader;
    private readonly IEnumerable<IWordsProcessor> wordsProcessors;

    public Client(
        IWordsLoader wordsLoader,
        IEnumerable<IWordsProcessor> wordsProcessors,
        ICloudCreator creator)
    {
        this.wordsLoader = wordsLoader;
        this.wordsProcessors = wordsProcessors;
        this.creator = creator;
    }

    public void Execute(string resultFilepath)
    {
        var words = wordsLoader.Load();
        foreach (var processor in wordsProcessors)
            words = processor.Process(words);
        var bitmap = creator.CreateTagCloud(words);
        bitmap.Save(resultFilepath);
    }
}