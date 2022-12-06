namespace CircularCloudLayouter.WeightedLayouter.Forming.Standard;

public class EllipseFormFactor : FormFactor
{
    public EllipseFormFactor(double widthToHeightRatio = 1) : base(widthToHeightRatio)
    {
    }

    public override EllipseFormFactor WithRatio(double widthToHeightRatio) =>
        new(widthToHeightRatio);


    public override double GetSegmentScore(int weight, double distToCenter) =>
        1d / (weight * weight + 1.4 * distToCenter * distToCenter);

    protected override int InternalCalculatePreferredStart(int min, int max, int sideLength, int middle) =>
        PreferredStartCalculators.CloserToMiddle(min, max, sideLength, middle);
}