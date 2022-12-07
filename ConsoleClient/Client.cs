using System.Drawing;
using TagCloud;
using TagCloud.Abstractions;

namespace ConsoleClient;

public class Client
{
    private readonly IWordsLoader wordsLoader;
    private readonly IWordsProcessor wordsProcessor;
    private readonly ICloudCreator creator;

    public Client(
        IWordsLoader wordsLoader,
        IWordsProcessor wordsProcessor,
        ICloudCreator creator)
    {
        this.wordsLoader = wordsLoader;
        this.wordsProcessor = wordsProcessor;
        this.creator = creator;
    }

    public void Execute()
    {
        var words = wordsLoader.Load();
        words = wordsProcessor.Process(words);
        var bitmap = creator.CreateTagCloud(words);
        bitmap.Save("result.png");
    }
}