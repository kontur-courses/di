using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Visualization
{
    public interface IVisualizer
    {
        void VisualizeCloud(Graphics graphics, Point cloudCenter, List<Rectangle> rectangles);

        void VisualizeDebuggingMarkup(Graphics graphics, Size imgSize, Point cloudCenter, int cloudCircleRadius);
    }
}