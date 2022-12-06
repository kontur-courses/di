using System.Drawing;
using CircularCloudLayouter.WeightedLayouter.Forming;
using CircularCloudLayouter.WeightedLayouter.SideLayouters;

namespace CircularCloudLayouter.WeightedLayouter;

public class WeightedTagCloudLayouter : ITagCloudLayouter
{
    private readonly Point _center;
    private readonly WeightedSideLayouter[] _sideLayouters;

    public int RectanglesPlaced { get; private set; }

    public WeightedTagCloudLayouter(Point center, FormFactor formFactor)
    {
        _center = center;
        _sideLayouters = new WeightedSideLayouter[]
        {
            new RightSideLayouter(_center, formFactor),
            new LeftSideLayouter(_center, formFactor),
            new TopSideLayouter(_center, formFactor),
            new BottomSideLayouter(_center, formFactor)
        };
    }

    public Rectangle PutNextRectangle(Size rectSize)
    {
        if (rectSize.Height <= 0 || rectSize.Width <= 0)
            throw new ArgumentException($"{nameof(rectSize.Width)} and {nameof(rectSize.Height)} cannot be zero!");

        var rect = RectanglesPlaced == 0
            ? new Rectangle(_center - rectSize / 2, rectSize)
            : _sideLayouters
                .MinBy(layouter => layouter.CalculateCoefficient())!
                .GetNextRectangle(rectSize);

        Parallel.ForEach(
            _sideLayouters,
            layouter => layouter.UpdateWeights(rect)
        );

        RectanglesPlaced++;
        return rect;
    }
}