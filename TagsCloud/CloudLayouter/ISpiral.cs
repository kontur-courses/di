using System.Drawing;

namespace TagsCloud.CloudLayouter;

public interface ISpiral
{
    Point GetPoint();
    void Init(Point center);
}