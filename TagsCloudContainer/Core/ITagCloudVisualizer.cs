using System.Drawing;

namespace TagsCloudContainer.Core
{
    public interface ITagCloudVisualizer
    {
        Bitmap GetTagCloudBitmap(string textFileName);
    }
}