using System.Drawing;

namespace TagsCloudVisualization.CloudLayouter;

public interface ISpiralFormula
{
    PointF GetPoint(PointF center, float length);
}