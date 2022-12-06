namespace CircularCloudLayouter.WeightedLayouter.Forming;

public abstract class FormFactor
{
    protected FormFactor(double widthToHeightRatio)
    {
        if (widthToHeightRatio <= 0)
            throw new ArgumentException($"{nameof(widthToHeightRatio)} should be positive!");
        WidthToHeightRatio = widthToHeightRatio;
    }

    public double WidthToHeightRatio { get; }

    public int GetPreferredStart(int min, int max, int sideLength, int middle)
    {
        if (max - min < sideLength)
            throw new ArgumentException("Not enough space to place side!");
        return InternalCalculatePreferredStart(min, max, sideLength, middle);
    }

    public abstract double GetSegmentScore(int weight, double distToCenter);

    public abstract FormFactor WithRatio(double widthToHeightRatio);

    protected abstract int InternalCalculatePreferredStart(int min, int max, int sideLength, int middle);
}