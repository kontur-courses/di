using System.Drawing;
using TagsCloudContainer.Visualization.Measurers;

namespace TagsCloudContainer.Visualization.Painters
{
    public interface IPainter
    {
        ColorizedRectangle[] Colorize(Rectangle[] rectangles);

        ColorizedTag[] Colorize(Tag[] tags);
    }
}