using System.Collections.Generic;
using System.Drawing;
using TagCloud.Words.Tags;

namespace TagCloud.Visualization
{
    public interface IDrawer
    {
        void DrawCanvasBoundary(Graphics graphics, Size imgSize);

        void DrawAxis(Graphics graphics, Size imgSize, Point cloudCenter);

        void DrawCloudBoundary(Graphics graphics, Size imgSize, Point cloudCenter, int cloudCircleRadius);

        void DrawTags(Graphics graphics, IEnumerable<Tag> tags);
    }
}