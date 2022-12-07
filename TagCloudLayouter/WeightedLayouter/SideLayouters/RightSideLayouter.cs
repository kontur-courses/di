using System.Drawing;
using CircularCloudLayouter.Segments;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public class RightSideLayouter : ISideLayouter
{
    private readonly WeightedSideHelper _weightedSideHelper;
    private readonly Point _center;

    public RightSideLayouter(Point center, WeightedSideHelper weightedSideHelper)
    {
        _center = center;
        _weightedSideHelper = weightedSideHelper;
    }

    public double CalculateCoefficient() =>
        _weightedSideHelper.CalculateCoefficient(1);

    public Rectangle GetNextRectangle(Size rectSize)
    {
        var resPos = _weightedSideHelper.FindNextRectPos(rectSize.Height, _center.Y);
        return new Rectangle(
            _center.X + resPos.Relative,
            resPos.Absolute,
            rectSize.Width, rectSize.Height
        );
    }

    public void UpdateWeights(Rectangle rect)
    {
        _weightedSideHelper.UpdateWeights(new WeightedSegment(rect.Top, rect.Bottom, rect.Right - _center.X));
    }
}