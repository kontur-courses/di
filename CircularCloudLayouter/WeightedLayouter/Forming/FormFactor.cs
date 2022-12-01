namespace CircularCloudLayouter.WeightedLayouter.Forming;

public abstract class FormFactor
{
    protected FormFactor(double widthToHeightRatio)
    {
        WidthToHeightRatio = widthToHeightRatio;
    }

    public double WidthToHeightRatio { get; }

    public int GetPreferredStart(int min, int max, int sideLength, int middle)
    {
        if (max - min < sideLength)
            throw new ArgumentException("Not enough space to place side!");
        return CalculatePreferredStart(min, max, sideLength, middle);
    }

    public abstract double GetSegmentScore(int weight, double distToCenter);

    public abstract FormFactor WithRatio(double widthToHeightRatio);

    protected abstract int CalculatePreferredStart(int min, int max, int sideLength, int middle);
}