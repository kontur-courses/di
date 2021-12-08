using System.Drawing;

namespace TagsCloudContainer.Abstractions;

public interface IVisualizer
{
    Bitmap GetBitmap();
}