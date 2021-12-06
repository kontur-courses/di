using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization.Interfaces
{
    public interface IImageGenerator
    {
        Bitmap GenerateTagCloudBitmap(IOrderedEnumerable<ITag> tags);
        void SetImageSize(Size imageSize);
        void SetFont(Font newFont);
        void SetColors(Color color);
    }
}