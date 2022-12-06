namespace CircularCloudLayouter.WeightedLayouter.Forming.Standard;

public class RectangleFormFactor : FormFactor
{
    public RectangleFormFactor(double widthToHeightRatio = 1) : base(widthToHeightRatio)
    {
    }

    public override RectangleFormFactor WithRatio(double widthToHeightRatio) =>
        new(widthToHeightRatio);

    public override double GetSegmentScore(int weight, double distToCenter) =>
        1d / weight;

    protected override int InternalCalculatePreferredStart(int min, int max, int sideLength, int middle) =>
        PreferredStartCalculators.CloserToMiddle(min, max, sideLength, middle);
}