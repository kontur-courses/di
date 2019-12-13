using System.Drawing;

namespace TagsCloudApp.ImageSave
{
    public interface IImageSaver
    {
        void SaveImage(Bitmap bitmap, string filename);
    }
}
