using TagCloud.AppSettings;
using TagCloud.Drawer;
using TagCloud.FileReader;
using TagCloud.FileSaver;
using TagCloud.Filter;
using TagCloud.WordRanker;
using TagCloud.WordsPreprocessor;

namespace TagCloud.UserInterface;

public class ConsoleUI : IUserInterface
{
    private readonly IFileReader reader;
    private readonly ISaver saver;
    private readonly IDrawer drawer;
    private readonly IWordRanker ranker;
    private readonly IFilter filter;
    private readonly IPreprocessor preprocessor;

    public ConsoleUI(IFileReader reader, ISaver saver, IDrawer drawer, IWordRanker ranker, IFilter filter,
        IPreprocessor preprocessor)
    {
        this.reader = reader;
        this.saver = saver;
        this.drawer = drawer;
        this.ranker = ranker;
        this.filter = filter;
        this.preprocessor = preprocessor;
    }

    public void Run(IAppSettings appSettings)
    {
        var words = reader.ReadLines(appSettings.InputPath);
        var preprocessed = preprocessor.HandleWords(words);
        var filtered = filter.FilterWords(preprocessed);
        var ranked = ranker.RankWords(filtered);
        using var bitmap = drawer.DrawTagCloud(ranked);
        saver.Save(bitmap, appSettings.OutputPath, appSettings.ImageExtension);
    }
}