using System.Drawing;
using CircularCloudLayouter.Segments;
using CircularCloudLayouter.WeightedLayouter.Forming;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public class TopSideLayouter : WeightedSideLayouter
{
    public TopSideLayouter(Point center, FormFactor formFactor) : base(center, formFactor)
    {
    }

    protected override double RatioCoefficient => FormFactor.WidthToHeightRatio;

    public override Rectangle GetNextRectangle(Size rectSize)
    {
        var resPos = FindNextRectPos(rectSize.Width, Center.X);
        return new Rectangle(
            resPos.Absolute,
            Center.Y - rectSize.Height - resPos.Relative,
            rectSize.Width, rectSize.Height
        );
    }

    protected override WeightedSegment ParseWeights(Rectangle rect) =>
        new(rect.Left, rect.Right, Center.Y - rect.Top);
}