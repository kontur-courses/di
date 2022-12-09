using System.Collections;

namespace CircularCloudLayouter.Domain.Segments;

public class WeightedCollection :
    ICollection<WeightedSegment>,
    IReadOnlyCollection<WeightedSegment>
{
    private readonly IWeightedSegmentsOptimizer _optimizer;
    private readonly LinkedList<WeightedSegment> _segments = new();

    public int Start =>
        _segments.Count > 0
            ? _segments.First!.Value.Start
            : throw new InvalidOperationException("No segments added!");

    public int End =>
        _segments.Count > 0
            ? _segments.Last!.Value.End
            : throw new InvalidOperationException("No segments added!");

    public int FullLength => End - Start;

    public int MaxWeight { get; private set; }

    public int Count => _segments.Count;

    public bool IsReadOnly => false;

    public WeightedCollection(IWeightedSegmentsOptimizer optimizer)
    {
        _optimizer = optimizer;
    }

    public int WeightAt(int point) =>
        _segments.Where(s => s.Start <= point && s.End >= point).Max(s => s.Weight);


    public void UpdateGreaterWeights(WeightedSegment newSegment)
    {
        if (newSegment.Length == 0)
            return;

        MaxWeight = Math.Max(MaxWeight, newSegment.Weight);
        if (_segments.Count == 0)
        {
            _segments.AddFirst(newSegment);
        }
        else if (newSegment.End <= Start)
        {
            if (newSegment.End < Start)
                _segments.AddFirst(new WeightedSegment(newSegment.End, Start));
            _segments.AddFirst(newSegment);
        }
        else if (newSegment.Start >= End)
        {
            if (newSegment.Start > End)
                _segments.AddLast(new WeightedSegment(End, newSegment.Start));
            _segments.AddLast(newSegment);
        }
        else
        {
            InsertWithIntersectionsHandling(newSegment);
        }
    }

    private void InsertWithIntersectionsHandling(WeightedSegment newSegment)
    {
        var intersections = GetNodesIntersectedWith(newSegment).ToList();
        var added = _segments.AddBefore(intersections.First(), newSegment);

        foreach (var node in intersections)
        {
            if (newSegment.Weight < node.Value.Weight)
            {
                added.Value = added.Value.WithEnd(Math.Max(node.Value.Start, added.Value.Start));
                if (added.Value.Length == 0)
                    _segments.Remove(added);

                if (newSegment.End > node.Value.End)
                    added = _segments.AddAfter(node, newSegment.WithStart(node.Value.End));
            }
            else
            {
                if (node.Value.Start < newSegment.Start)
                    _segments.AddBefore(added, node.Value.WithEnd(added.Value.Start));

                node.Value = node.Value.WithStart(Math.Min(added.Value.End, node.Value.End));
                if (node.Value.Length == 0)
                    _segments.Remove(node);
            }
        }
    }

    private IEnumerable<LinkedListNode<WeightedSegment>> GetNodesIntersectedWith(Segment segment)
    {
        var current = _segments.First;
        while (current is not null && segment.Start >= current.Value.End)
            current = current.Next;
        while (current is not null && segment.End > current.Value.Start)
        {
            yield return current;
            current = current.Next;
        }
    }

    public void OptimizeWeights() => _optimizer.OptimizeWeights(_segments);

    public IEnumerator<WeightedSegment> GetEnumerator() =>
        _segments.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    void ICollection<WeightedSegment>.Add(WeightedSegment item) =>
        UpdateGreaterWeights(item);

    public bool Contains(WeightedSegment item) =>
        _segments
            .Any(segment =>
                segment.Start <= item.Start &&
                segment.End >= item.End &&
                segment.Weight == item.Weight
            );

    public bool Remove(WeightedSegment item)
    {
        var node = _segments.Find(item);
        if (node is null)
            return false;

        var removedWeight = node.Value.Weight;

        if (node.Previous is null || node.Next is null)
            _segments.Remove(node);
        else
            node.Value = new WeightedSegment(node.Value.Start, node.Value.End);

        if (removedWeight == MaxWeight)
            MaxWeight = _segments.Max(s => s.Weight);
        return true;
    }

    public void Clear()
    {
        _segments.Clear();
        MaxWeight = 0;
    }

    public void CopyTo(WeightedSegment[] array, int arrayIndex) =>
        _segments.CopyTo(array, arrayIndex);
}