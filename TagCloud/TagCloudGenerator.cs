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
    private readonly TextMeasurer measurer;

    public TagCloudGenerator(ITextHandler handler, CircularCloudLayouter layouter, ICloudDrawer drawer, TextMeasurer measurer)
    {
        this.handler = handler;
        this.layouter = layouter;
        this.drawer = drawer;
        this.measurer = measurer;
    }

    public void Generate()
    {
        var rectangles = new List<TextRectangle>();
        
        foreach (var (word, count) in handler.Handle().OrderByDescending(pair => pair.Value))
        {
            var (size, font) = measurer.GetTextRectangleSize(word, count);
            rectangles.Add(new TextRectangle(
                layouter.PutNextRectangle(size),
                word,
                font
            ));
        }
        drawer.Draw(rectangles);
    }
}