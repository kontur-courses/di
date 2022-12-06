namespace CircularCloudLayouter.WeightedLayouter.Forming.Standard;

public class CrossFormFactor : FormFactor
{
    public CrossFormFactor(double widthToHeightRatio = 1) : base(widthToHeightRatio)
    {
    }

    public override CrossFormFactor WithRatio(double widthToHeightRatio) =>
        new(widthToHeightRatio);

    public override double GetSegmentScore(int weight, double distToCenter) =>
        weight / (weight * weight + 1.4 * distToCenter * distToCenter);

    protected override int InternalCalculatePreferredStart(int min, int max, int sideLength, int middle) =>
        PreferredStartCalculators.CloserToMiddle(min, max, sideLength, middle);
}