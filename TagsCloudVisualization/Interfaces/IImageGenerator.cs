#region

using System.Drawing;
using System.Linq;

#endregion

namespace TagsCloudVisualization.Interfaces
{
    public interface IImageGenerator
    {
        Bitmap GenerateTagCloudBitmap(IOrderedEnumerable<ITag> tags);
    }
}