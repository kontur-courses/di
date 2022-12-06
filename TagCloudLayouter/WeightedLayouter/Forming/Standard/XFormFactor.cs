namespace CircularCloudLayouter.WeightedLayouter.Forming.Standard;

public class XFormFactor : FormFactor
{
    public XFormFactor(double widthToHeightRatio = 1) : base(widthToHeightRatio)
    {
    }

    public override XFormFactor WithRatio(double widthToHeightRatio) =>
        new(widthToHeightRatio);

    public override double GetSegmentScore(int weight, double distToCenter) =>
        distToCenter / (weight * weight + distToCenter * distToCenter);

    protected override int InternalCalculatePreferredStart(int min, int max, int sideLength, int middle) =>
        PreferredStartCalculators.CloserToEdges(min, max, sideLength, middle);
}