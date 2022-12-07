using System.Drawing;
using CircularCloudLayouter.Segments;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public class BottomSideLayouter : ISideLayouter
{
    private readonly WeightedSideHelper _weightedSideHelper;
    private readonly Point _center;

    public BottomSideLayouter(Point center, WeightedSideHelper weightedSideHelper)
    {
        _center = center;
        _weightedSideHelper = weightedSideHelper;
    }

    public double CalculateCoefficient() =>
        _weightedSideHelper.CalculateCoefficient(_weightedSideHelper.FormFactor.WidthToHeightRatio);

    public Rectangle GetNextRectangle(Size rectSize)
    {
        var resPos = _weightedSideHelper.FindNextRectPos(rectSize.Width, _center.X);
        return new Rectangle(
            resPos.Absolute,
            _center.Y + resPos.Relative,
            rectSize.Width, rectSize.Height
        );
    }

    public void UpdateWeights(Rectangle rect)
    {
        _weightedSideHelper.UpdateWeights(new WeightedSegment(rect.Left, rect.Right, rect.Bottom - _center.Y));
    }
}