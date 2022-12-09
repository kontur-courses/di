using CircularCloudLayouter.Domain;
using CircularCloudLayouter.WeightedLayouter.Forming;
using CircularCloudLayouter.WeightedLayouter.SideLayouters;

namespace CircularCloudLayouter.WeightedLayouter;

public class WeightedTagCloudLayouter : ITagCloudLayouter
{
    private readonly ImmutablePoint _center;
    private readonly ISideLayouter[] _sideLayouters;

    public int RectanglesPlaced { get; private set; }

    public WeightedTagCloudLayouter(ImmutablePoint center, FormFactor formFactor)
    {
        _center = center;
        _sideLayouters = new ISideLayouter[]
        {
            new RightSideLayouter(_center, new WeightedSideHelper(formFactor)),
            new LeftSideLayouter(_center, new WeightedSideHelper(formFactor)),
            new TopSideLayouter(_center, new WeightedSideHelper(formFactor)),
            new BottomSideLayouter(_center, new WeightedSideHelper(formFactor))
        };
    }

    public ImmutableRectangle PutNextRectangle(ImmutableSize rectSize)
    {
        if (rectSize.Height <= 0 || rectSize.Width <= 0)
            throw new ArgumentException($"{nameof(rectSize.Width)} and {nameof(rectSize.Height)} cannot be zero!");

        var rect = RectanglesPlaced == 0
            ? new ImmutableRectangle(_center - rectSize / 2, rectSize)
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