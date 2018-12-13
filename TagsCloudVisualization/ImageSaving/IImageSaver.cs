using System.Drawing;

namespace TagsCloudVisualization.ImageSaving
{
    public interface IImageSaver
    {
        void SaveImage(Image image, string path);
        string[] SupportedTypes();
    }
}
