using System.Drawing;

namespace TagsCloud.Interfaces
{
    public interface IImageSaver
    {
        void SaveImage(Image image, string path);
    }
}
