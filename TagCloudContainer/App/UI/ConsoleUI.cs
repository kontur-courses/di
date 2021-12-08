using TagCloud.Infrastructure.Common;
using TagCloud.Infrastructure.FileReader;
using TagCloud.Infrastructure.Filter;
using TagCloud.Infrastructure.Lemmatizer;
using TagCloud.Infrastructure.Painter;
using TagCloud.Infrastructure.Saver;
using TagCloud.Infrastructure.WordWeigher;

namespace TagCloud.App.UI;

public class ConsoleUI : IUserInterface
{
    private readonly IFileReader fileReader;
    private readonly IFilter filter;
    private readonly ILemmatizer lemmatizer;
    private readonly IPainter painter;
    private readonly IImageSaver saver;
    private readonly IWordWeigher weigher;

    public ConsoleUI(IFileReader fileReader, IPainter painter, IWordWeigher weigher, IImageSaver saver, ILemmatizer lemmatizer, IFilter filter)
    {
        this.fileReader = fileReader;
        this.painter = painter;
        this.weigher = weigher;
        this.saver = saver;
        this.lemmatizer = lemmatizer;
        this.filter = filter;
    }

    public void Run(IAppSettings settings)
    {
        var lines = fileReader.GetLines(settings.InputPath);
        var lemmas = lemmatizer.GetLemmas(lines);
        var filtered = filter.FilterWords(lemmas);
        var weightedWords = weigher.GetWeightedWords(filtered);
        using var bitmap = painter.CreateImage(weightedWords);
        saver.Save(bitmap, settings.OutputPath, settings.OutputFormat);
    }
}