using System.Collections.Generic;
using System.Drawing;

namespace TagCloud.Algorithms.AlgorithmFromTDD.Visualization
{
    public interface IVisualizer
    {
        void VisualizeCloud(Graphics g, Point cloudCenter, List<Rectangle> rectangles);

        void VisualizeDebuggingMarkup(Graphics g, Size imgSize, Point cloudCenter, int cloudCircleRadius);
    }
}