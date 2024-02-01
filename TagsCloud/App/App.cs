using TagsCloud.Layouters;
using TagsCloud.TagsCloudPainters;
using TagsCloud.TextReaders;

namespace TagsCloud.App;

public class App : IApp
{
    private readonly IWordsProvider wordsProvider;
    private readonly ILayouter layouter;
    private readonly IPainter painter;

    public App(IWordsProvider wordsProvider, ILayouter layouter, IPainter painter)
    {
        this.wordsProvider = wordsProvider;
        this.layouter = layouter;
        this.painter = painter;
    }

    public void Run()
    {
        layouter.CreateTagCloud(wordsProvider.GetWords());
        painter.DrawCloud(layouter);
    }
}