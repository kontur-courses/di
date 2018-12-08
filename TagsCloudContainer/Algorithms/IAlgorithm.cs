using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Algorithms
{
    public interface IAlgorithm
    {
        IReadOnlyDictionary<string, (Rectangle, int)> GenerateRectanglesSet(IReadOnlyDictionary<string, int> processedWords);
    }
}