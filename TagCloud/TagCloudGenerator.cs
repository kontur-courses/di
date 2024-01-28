using System.Drawing;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.TextHandlers;
using TagCloudApplication;
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
        
        foreach (var (word, count) in handler.Handle().OrderByDescending(pair => pair.Value))
        {
            var fontSize = drawer.FontSize * count;
            var size = drawer.GetTextRectangleSize(word, fontSize);
            rectangles.Add(new TextRectangle(
                layouter.PutNextRectangle(size),
                word,
                new Font(FontFamily.GenericSerif, fontSize)
            ));
        }
        drawer.Draw(rectangles);
    }
}