using System.Drawing;

namespace TagsCloudVisualization.ImageSavers;

public interface IImageSaver
{
    void SaveImage(Bitmap bitmap);
}
