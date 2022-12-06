using System.Drawing;
using CircularCloudLayouter.Segments;
using CircularCloudLayouter.WeightedLayouter.Forming;

namespace CircularCloudLayouter.WeightedLayouter.SideLayouters;

public abstract class WeightedSideLayouter
{
    private const int NeighboursSpace = 2;

    private static readonly IWeightedSegmentsOptimizer Optimizer =
        new SimpleWeightedSegmentsOptimizer(2 * NeighboursSpace, NeighboursSpace);

    private readonly WeightedCollection _sideWeights = new(Optimizer);

    protected readonly Point Center;
    protected readonly FormFactor FormFactor;

    protected WeightedSideLayouter(Point center, FormFactor formFactor)
    {
        Center = center;
        FormFactor = formFactor;
    }

    public double CalculateCoefficient() =>
        _sideWeights.MaxWeight * RatioCoefficient;

    protected abstract double RatioCoefficient { get; }

    public abstract Rectangle GetNextRectangle(Size rectSize);

    public void UpdateWeights(Rectangle rect)
    {
        var newWeights = ParseWeights(rect);
        if (newWeights.Weight < 0)
            return;
        _sideWeights.UpdateGreaterWeights(newWeights);
        _sideWeights.OptimizeWeights();
    }

    protected abstract WeightedSegment ParseWeights(Rectangle rectangle);

    protected (int Absolute, int Relative) FindNextRectPos(int sideLength, int middle)
    {
        sideLength += 2 * NeighboursSpace;

        var mergedSegments = new Queue<WeightedSegment>();
        var mergedWeight = int.MinValue;

        var bestScore = double.MinValue;
        var bestSegment = new WeightedSegment(0, 0, int.MaxValue);

        foreach (var segment in SegmentsWithRequiredOffset(sideLength))
        {
            mergedWeight = HandleNewSegment(segment, mergedSegments, sideLength, mergedWeight);
            var min = mergedSegments.Peek().Start;
            var max = segment.End;

            if (max - min < sideLength)
                continue;

            var mergedStart = FormFactor.GetPreferredStart(min, max, sideLength, middle);

            var mergedDistToCenter = GetDistToCenter(mergedStart, sideLength, middle);
            var score = FormFactor.GetSegmentScore(mergedWeight, mergedDistToCenter);

            if (score <= bestScore)
                continue;

            bestSegment = new WeightedSegment(mergedStart, mergedStart + sideLength, mergedWeight);
            bestScore = score;
        }

        return GetResultPos(bestSegment);
    }

    private static int HandleNewSegment(
        WeightedSegment segment,
        Queue<WeightedSegment> mergedSegments,
        int sideLength, int mergedWeight
    )
    {
        mergedSegments.Enqueue(segment);
        mergedWeight = Math.Max(mergedWeight, segment.Weight);

        if (segment.End - mergedSegments.Peek().End > sideLength && mergedSegments.Dequeue().Weight == mergedWeight)
            mergedWeight = mergedSegments.Max(sgm => sgm.Weight);

        return mergedWeight;
    }

    private static double GetDistToCenter(int start, int sideLength, int middle) =>
        Math.Abs(middle - (start + sideLength / 2d));

    private static (int Absolute, int Relative) GetResultPos(WeightedSegment segment) =>
        (segment.Start + NeighboursSpace, segment.Weight + NeighboursSpace);

    private IEnumerable<WeightedSegment> SegmentsWithRequiredOffset(int minLength)
    {
        var offsetLength = (int) Math.Ceiling((minLength - _sideWeights.FullLength) / 2d);

        if (offsetLength > 0)
            yield return new WeightedSegment(_sideWeights.Start - offsetLength, _sideWeights.Start);

        foreach (var segment in _sideWeights)
            yield return segment;

        if (offsetLength > 0)
            yield return new WeightedSegment(_sideWeights.End, _sideWeights.End + offsetLength);
    }
}