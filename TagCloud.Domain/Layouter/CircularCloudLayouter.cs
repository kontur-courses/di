using TagCloud.Domain.Layout.Interfaces;
using TagCloud.Domain.Layouter.Interfaces;
using TagCloud.Domain.Settings;
using TagCloud.Utils.Extensions;

namespace TagCloud.Domain.Layouter;

public class CircularCloudLayouter : ICloudLayouter
{
    private readonly LayoutSettings settings;
    private readonly ILayout layout;
    private readonly List<Rectangle> rectangles = new();
    public Point Center { get; private set; }

    public CircularCloudLayouter(LayoutSettings settings, ILayout layout)
    {
        this.settings = settings;
        this.layout = layout;
    }

    public Rectangle PutNextRectangle(Size rectangleSize)
    {
        Center = new Point(settings.Dimensions.GetGreaterHalf());
        
        if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
            throw new ArgumentException("Width and height of rectangle must be positive");

        var offsetBeforeCenter = rectangleSize.GetGreaterHalf();

        foreach (var coord in layout.GetNextCoord(Center))
        {
            var now = new Rectangle(coord - offsetBeforeCenter, rectangleSize);
            
            if (!CanPutRectangle(now))
                continue;

            PutRectangle(now);

            return now;
        }

        throw new IndexOutOfRangeException("Cannot find suitable place on the layout");
    }
    
    private bool CanPutRectangle(Rectangle rectangle)
    {
        return !rectangles.Any(rectangle.IntersectsWith);
    }

    private void PutRectangle(Rectangle rectangle)
    {
        rectangles.Add(rectangle);
    }
}