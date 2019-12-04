using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagPainter
    {
        Color GetTagColor();
    }
}