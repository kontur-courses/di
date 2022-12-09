namespace CircularCloudLayouter.Domain.Segments;

public class Segment : IEquatable<Segment>
{
    public int Start { get; }

    public int End { get; }

    public int Length => End - Start;

    public Segment(int start, int end)
    {
        if (start > end)
            throw new ArgumentException("Start cannot be less than end!");
        Start = start;
        End = end;
    }

    public virtual Segment WithStart(int start) => new(start, End);
    public virtual Segment WithEnd(int end) => new(Start, end);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Segment) obj);
    }

    public bool Equals(Segment? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Start == other.Start && End == other.End;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
}