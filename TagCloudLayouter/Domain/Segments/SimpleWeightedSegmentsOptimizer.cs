namespace CircularCloudLayouter.Domain.Segments;

public class SimpleWeightedSegmentsOptimizer : IWeightedSegmentsOptimizer
{
    private readonly int _maxLengthToRemove;
    private readonly int _maxWeightDeltaToCombine;

    public SimpleWeightedSegmentsOptimizer() : this(0, 0)
    {
    }

    public SimpleWeightedSegmentsOptimizer(int maxLengthToRemove, int maxWeightDeltaToCombine)
    {
        _maxLengthToRemove = maxLengthToRemove;
        _maxWeightDeltaToCombine = maxWeightDeltaToCombine;
    }

    public void OptimizeWeights(LinkedList<WeightedSegment> segments)
    {
        var current = segments.First;
        while (current?.Next is not null)
        {
            if (current.Previous is null)
            {
                current = current.Next;
                continue;
            }

            if (TryMaxLengthOptimization(segments, current))
                continue;
            if (TryWeightDeltaOptimization(segments, current))
                continue;
            current = current.Next;
        }
    }

    private bool TryMaxLengthOptimization(LinkedList<WeightedSegment> segments, LinkedListNode<WeightedSegment> node)
    {
        if (node.Value.Length > _maxLengthToRemove)
            return false;

        if (node.Previous!.Value.Weight >= node.Value.Weight && node.Previous.Value.Weight < node.Next!.Value.Weight)
        {
            CombineWithPrev(segments, node, node.Previous.Value.Weight);
            return true;
        }

        if (node.Next!.Value.Weight >= node.Value.Weight)
        {
            CombineWithNext(segments, node, node.Next.Value.Weight);
            return true;
        }

        return false;
    }

    private bool TryWeightDeltaOptimization(LinkedList<WeightedSegment> segments, LinkedListNode<WeightedSegment> node)
    {
        if (Math.Abs(node.Value.Weight - node.Next!.Value.Weight) > _maxWeightDeltaToCombine)
            return false;

        CombineWithNext(segments, node, Math.Max(node.Value.Weight, node.Next.Value.Weight));
        return true;
    }

    private static void CombineWithPrev(
        LinkedList<WeightedSegment> segments,
        LinkedListNode<WeightedSegment> node,
        int combinedWeight
    )
    {
        node.Value = new WeightedSegment(node.Previous!.Value.Start, node.Value.End, combinedWeight);
        segments.Remove(node.Previous);
    }

    private static void CombineWithNext(
        LinkedList<WeightedSegment> segments,
        LinkedListNode<WeightedSegment> node,
        int combinedWeight
    )
    {
        node.Value = new WeightedSegment(node.Value.Start, node.Next!.Value.End, combinedWeight);
        segments.Remove(node.Next);
    }
}