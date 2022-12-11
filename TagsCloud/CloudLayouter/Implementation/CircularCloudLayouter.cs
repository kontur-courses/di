using System.Drawing;
using TagsCloud.Creators;
using TagsCloud.Creators.Implementation;
using TagsCloud.FigurePatterns;
using TagsCloud.FigurePatterns.Implementation;

namespace TagsCloud.CloudLayouter.Implementation;

public sealed class CircularCloudLayouter : CloudLayouter<Rectangle>
{
    private readonly ICreator<Rectangle> creator;
    private readonly IFigurePatternPointProvider figurePatternPointProvider;

    public CircularCloudLayouter(IFigurePatternPointProvider provider, ICreator<Rectangle> creator)
    {
        figurePatternPointProvider = provider;
        this.creator = creator;
    }

    public CircularCloudLayouter(Point center, double figureStep = 1)
    {
        figurePatternPointProvider = new SpiralPatterPointProvider(center, figureStep);
        creator = new RectangleCreator();
    }

    public override Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Incorrect width or height", nameof(rectangleSize));
            
        var figure = GetNextFigure(rectangleSize);
        Figures.Add(figure);
        return figure;
    }

    private Rectangle GetNextFigure(Size size)
    {
        while (true)
        {
            var point = figurePatternPointProvider.GetNextPoint();
            var figure = creator.Place(point, size);
            if (Figures.Any(fig => fig.IntersectsWith(figure)))
                continue;

            figurePatternPointProvider.Restart();
            return figure;
        }
    }
}
