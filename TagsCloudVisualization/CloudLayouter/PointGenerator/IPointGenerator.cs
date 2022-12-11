using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter.PointGenerator;

public interface IPointGenerator
{
    Point Next();
}