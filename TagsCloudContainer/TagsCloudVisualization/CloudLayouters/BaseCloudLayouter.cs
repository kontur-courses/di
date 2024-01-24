using System.Drawing;
using TagsCloudVisualization.PointsProviders;

namespace TagsCloudVisualization.CloudLayouters;

public abstract class BaseCloudLayouter<TPointsProvider> : ITagsCloudLayouter
    where TPointsProvider : IPointsProvider
{
    private readonly List<Rectangle> rectangles;
    public IEnumerable<Rectangle> Rectangles => rectangles;
    protected readonly TPointsProvider PointsProvider;
    public Point Center => PointsProvider.Start;

    protected BaseCloudLayouter(TPointsProvider pointsProvider)
    {
        if (pointsProvider is null)
            throw new ArgumentNullException("PointsProvider can't be null");
        
        PointsProvider = pointsProvider;
        rectangles = new List<Rectangle>();
    }
    
    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        if (rectangleSize.Height <= 0 || rectangleSize.Width <= 0)
            throw new ArgumentException("Rectangle width and height should be positive");
        
        var rectPosition = FindPositionForRectangle(rectangleSize);
        
        var rectangle = new Rectangle(rectPosition, rectangleSize);
        rectangles.Add(rectangle);
        return rectangle;
    }

    protected abstract Point FindPositionForRectangle(Size rectangleSize);

    protected virtual bool IsPlaceSuitableForRectangle(Rectangle rectangle)
    {
        return rectangles.All(x => !x.IntersectsWith(rectangle));
    }

    public void Dispose()
    {
        rectangles.Clear();
    }
}