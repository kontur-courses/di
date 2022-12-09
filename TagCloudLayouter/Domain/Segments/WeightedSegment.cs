namespace CircularCloudLayouter.Domain.Segments;

public class WeightedSegment : Segment, IEquatable<WeightedSegment>
{
    public int Weight { get; }
    public int FullWeight => Length * Weight;

    public WeightedSegment(int start, int end, int weight = 0) : base(start, end)
    {
        Weight = weight;
    }

    public WeightedSegment(Segment segment, int weight) : this(segment.Start, segment.End, weight)
    {
    }

    public override WeightedSegment WithStart(int start) => new(base.WithStart(start), Weight);
    public override WeightedSegment WithEnd(int end) => new(base.WithEnd(end), Weight);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((WeightedSegment) obj);
    }

    public bool Equals(WeightedSegment? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return base.Equals(other) && Weight == other.Weight;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(base.GetHashCode(), Weight);
    }
}