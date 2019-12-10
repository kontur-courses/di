using System.Collections.Generic;
using System.Drawing;
using TagCloud.Infrastructure;

namespace TagCloud.Visualization
{
    public interface ITagCloudGenerator
    {
        Bitmap GetTagCloudBitmap(IEnumerable<Word> words);
    }
}
