using System.Drawing;
using CircularCloudLayouter.Segments;
using CircularCloudLayouter.WeightedLayouter.Forming;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public class LeftSideLayouter : WeightedSideLayouter
{
    public LeftSideLayouter(Point center, FormFactor formFactor) : base(center, formFactor)
    {
    }

    protected override double RatioCoefficient => 1;

    public override Rectangle GetNextRectangle(Size rectSize)
    {
        var resPos = FindNextRectPos(rectSize.Height, Center.Y);
        return new Rectangle(
            Center.X - rectSize.Width - resPos.Relative,
            resPos.Absolute,
            rectSize.Width, rectSize.Height
        );
    }

    protected override WeightedSegment ParseWeights(Rectangle rect) =>
        new(rect.Top, rect.Bottom, Center.X - rect.Left);
}