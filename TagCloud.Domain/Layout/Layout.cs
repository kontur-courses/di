using TagCloud.Domain.Layout.Interfaces;

namespace TagCloud.Domain.Layout;

public class Layout : ILayout
{
    public IEnumerable<Point> GetNextCoord(Point center)
    {
        yield return center + new Size(0, 0);
        var offset = 1;

        while (true)
        {
            for (var dx = -offset; dx <= offset; dx++)
            for (var dy = -offset; dy <= offset; dy++)
                if (Math.Abs(dx) == offset || Math.Abs(dy) == offset)
                    yield return center + new Size(dx, dy);

            offset++;
        }
        
        // ReSharper disable once IteratorNeverReturns
    }
}