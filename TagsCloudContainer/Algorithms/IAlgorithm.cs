using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithms
{
    public interface IAlgorithm
    {
        Dictionary<string, (Rectangle, int)> GenerateRectanglesSet(IEnumerable<KeyValuePair<string, int>> processedWords);
    }
}