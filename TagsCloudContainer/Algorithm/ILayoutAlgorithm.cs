using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithm
{
    public interface ILayoutAlgorithm
    {
        IEnumerable<(string, Rectangle)> GetLayout(IEnumerable<string> words, Size pictureSize);
    }
}