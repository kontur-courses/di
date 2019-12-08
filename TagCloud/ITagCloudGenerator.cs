using System.Collections.Generic;
using System.Drawing;

namespace TagCloud
{
    public interface ITagCloudGenerator
    {
        Bitmap GetTagCloudBitmap();
    }
}
