using System.Collections.Generic;
using System.Drawing;
using TagCloud.Words.Tags;

namespace TagCloud.Visualization
{
    public interface IVisualizer
    {
        void VisualizeCloudOnImage(Image image, Point cloudCenter, IEnumerable<Tag> tags);

        void VisualizeDebuggingMarkupOnImage(Image image, Point cloudCenter, int cloudCircleRadius);
    }
}