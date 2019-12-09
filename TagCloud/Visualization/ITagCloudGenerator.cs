using System.Drawing;

namespace TagCloud.Visualization
{
    public interface ITagCloudGenerator
    {
        Bitmap GetTagCloudBitmap();
    }
}
