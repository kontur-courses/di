using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithm
{
    public interface ILayoutAlgorithm
    {
        Dictionary<string, Rectangle> GetLayout(IEnumerable<string> words);
    }
}