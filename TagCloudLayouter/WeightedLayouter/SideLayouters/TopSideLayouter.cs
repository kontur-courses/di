using CircularCloudLayouter.Domain;
using CircularCloudLayouter.Domain.Segments;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public class TopSideLayouter : ISideLayouter
{
    private readonly WeightedSideHelper _weightedSideHelper;
    private readonly ImmutablePoint _center;

    public TopSideLayouter(ImmutablePoint center, WeightedSideHelper weightedSideHelper)
    {
        _center = center;
        _weightedSideHelper = weightedSideHelper;
    }

    public double CalculateCoefficient() =>
        _weightedSideHelper.CalculateCoefficient(_weightedSideHelper.FormFactor.WidthToHeightRatio);

    public ImmutableRectangle GetNextRectangle(ImmutableSize rectSize)
    {
        var resPos = _weightedSideHelper.FindNextRectPos(rectSize.Width, _center.X);
        return new ImmutableRectangle(
            resPos.Absolute,
            _center.Y - rectSize.Height - resPos.Relative,
            rectSize.Width, rectSize.Height
        );
    }

    public void UpdateWeights(ImmutableRectangle rect)
    {
        _weightedSideHelper.UpdateWeights(new WeightedSegment(rect.Left, rect.Right, _center.Y - rect.Top));
    }
}