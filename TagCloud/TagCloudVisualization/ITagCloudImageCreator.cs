using System.Collections.Generic;
using System.Drawing;

namespace TagCloudVisualization
{
    public interface ITagCloudImageCreator
    {
        Bitmap CreateTagCloudImage(
            IEnumerable<(Rectangle rectangle, string word)> wordPairs,
            ImageCreatingOptions options);
    }
}
