using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagMaker
    {
        Dictionary<Rectangle, (string, Font)> MakeTagRectangles(
            Dictionary<string, int> frequencyDict);
    }
}