using System.Collections.Generic;
using System.Drawing;

namespace TagCloudVisualization
{
    public interface ITagCloudImageCreator
    {
        TagCloudImage CreateTagCloudImage(IEnumerable<(Rectangle, string)> wordPairs, ImageCreatingOptions options);
    }
}
