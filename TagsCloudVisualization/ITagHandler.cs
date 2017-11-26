using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagHandler
    {
        Dictionary<Rectangle, (string, Font)> MakeTagRectangles(
            Dictionary<string, int> frequencyDict);
    }
}