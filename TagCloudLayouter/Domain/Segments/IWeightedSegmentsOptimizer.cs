namespace CircularCloudLayouter.Domain.Segments;

public interface IWeightedSegmentsOptimizer
{
    public void OptimizeWeights(LinkedList<WeightedSegment> segments);
}