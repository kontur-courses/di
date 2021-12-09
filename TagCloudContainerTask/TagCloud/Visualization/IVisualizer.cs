using System.Collections.Generic;
using System.Drawing;
using TagCloud.Words.Tags;

namespace TagCloud.Visualization
{
    public interface IVisualizer
    {
        void VisualizeCloud(Graphics graphics, Point cloudCenter, IEnumerable<Tag> tags);

        void VisualizeDebuggingMarkup(Graphics graphics, Size imgSize, Point cloudCenter, int cloudCircleRadius);
    }
}