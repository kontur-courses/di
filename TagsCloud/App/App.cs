using TagsCloud.Layouters;
using TagsCloud.TagsCloudPainters;
using TagsCloud.TextReaders;

namespace TagsCloud.App;

public class App:IApp
{
    private readonly ITextReader textReader;
    private readonly ILayouter layouter;
    private readonly IPainter painter;
    
    public App(ITextReader textReader,ILayouter layouter, IPainter painter)
    {
        this.textReader = textReader;
        this.layouter = layouter;
        this.painter = painter;
    }

    public void Run()
    {
        layouter.CreateTagCloud( textReader.GetWords());
        painter.DrawCloud(layouter);
    }
}