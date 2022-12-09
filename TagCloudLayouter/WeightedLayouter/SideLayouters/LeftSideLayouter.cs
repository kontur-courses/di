using CircularCloudLayouter.Domain;
using CircularCloudLayouter.Domain.Segments;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public class LeftSideLayouter : ISideLayouter
{
    private readonly WeightedSideHelper _weightedSideHelper;
    private readonly ImmutablePoint _center;

    public LeftSideLayouter(ImmutablePoint center, WeightedSideHelper weightedSideHelper)
    {
        _center = center;
        _weightedSideHelper = weightedSideHelper;
    }

    public double CalculateCoefficient() => 
        _weightedSideHelper.CalculateCoefficient(1);

    public ImmutableRectangle GetNextRectangle(ImmutableSize rectSize)
    {
        var resPos = _weightedSideHelper.FindNextRectPos(rectSize.Height, _center.Y);
        return new ImmutableRectangle(
            _center.X - rectSize.Width - resPos.Relative,
            resPos.Absolute,
            rectSize.Width, rectSize.Height
        );
    }

    public void UpdateWeights(ImmutableRectangle rect)
    {
        _weightedSideHelper.UpdateWeights(new WeightedSegment(rect.Top, rect.Bottom, _center.X - rect.Left));
    }
}