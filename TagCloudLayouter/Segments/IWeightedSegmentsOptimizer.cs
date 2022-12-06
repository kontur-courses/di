namespace CircularCloudLayouter.Segments;

public interface IWeightedSegmentsOptimizer
{
    public void OptimizeWeights(LinkedList<WeightedSegment> segments);
}