using System.Drawing;

namespace TagsCloudContainer;

public interface IImageDrawer
{
    Result<Bitmap> DrawnBitmap { get; }
    public Result<Bitmap> DrawImage();
}