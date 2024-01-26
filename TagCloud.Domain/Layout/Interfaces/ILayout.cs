namespace TagCloud.Domain.Layout.Interfaces;

public interface ILayout
{
    public IEnumerable<Point> GetNextCoord(Point center);
}