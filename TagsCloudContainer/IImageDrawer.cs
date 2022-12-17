using System.Drawing;

namespace TagsCloudContainer;

public interface IImageDrawer
{
    Bitmap DrawnBitmap { get; }
    public Result<Bitmap> DrawImage();
}