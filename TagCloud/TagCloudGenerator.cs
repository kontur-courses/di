using System.CodeDom.Compiler;
using System.Drawing;
using TagCloud.TextHandlers;
//TODO: перенести в TagCloud
using TagCloudTests;

namespace TagCloud;

public class TagCloudGenerator
{
    private readonly ITextHandler handler;
    private readonly CircularCloudLayouter layouter;
    private readonly ICloudDrawer drawer;

    public TagCloudGenerator(ITextHandler handler, CircularCloudLayouter layouter, ICloudDrawer drawer)
    {
        this.handler = handler;
        this.layouter = layouter;
        this.drawer = drawer;
    }

    public void Generate()
    {
        var rectangles = new List<TextRectangle>();
        
        foreach (var (word, count) in handler.Handle())
        {
            var size = drawer.GetTextRectangleSize(word, drawer.FontSize * count);
            rectangles.Add(new TextRectangle(
                layouter.PutNextRectangle(size),
                word,
                new Font(FontFamily.GenericSerif, drawer.FontSize * count)
            ));
        }
        drawer.Draw(rectangles);
    }
}