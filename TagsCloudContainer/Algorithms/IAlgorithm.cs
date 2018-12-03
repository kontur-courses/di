using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithms
{
    public interface IAlgorithm
    {
        Dictionary<string, (Rectangle, int)> GenerateRectanglesSet(IEnumerable<KeyValuePair<string, int>> take, int i, int i1, int i2, int i3);
    }
}