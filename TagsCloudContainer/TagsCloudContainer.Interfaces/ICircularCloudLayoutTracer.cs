using System.Drawing;

namespace TagsCloudContainer.Interfaces;

public interface ICircularCloudLayoutTracer
{
    void TraceRectangle(Rectangle rectangle);

    void TraceCirclePoint(Point point);

    void TraceShifting(Point from, Point to);
}