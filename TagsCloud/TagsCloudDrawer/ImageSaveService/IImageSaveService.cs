using System.Drawing;

namespace TagsCloudDrawer.ImageSaveService
{
    public interface IImageSaveService
    {
        void Save(string filename, Image image);
    }
}