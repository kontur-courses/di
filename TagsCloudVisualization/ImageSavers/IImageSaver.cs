using System.Drawing;

namespace TagsCloudVisualization.ImageSavers;

interface IImageSaver
{
    void SaveImage(Bitmap bitmap, string fileName, string pathToDirectory);
}
