using System.Drawing;

namespace TagsCloudDrawer.ImageSaveService
{
    public interface IImageSavior
    {
        void Save(string filename, Image image);
    }
}