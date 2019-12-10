using System.Drawing;
using System.Drawing.Imaging;

namespace TagsCloud.Interfaces
{
    public interface IImageSaver
    {
        void SaveImage(Image image, string path, ImageFormat formatResult);
    }
}
