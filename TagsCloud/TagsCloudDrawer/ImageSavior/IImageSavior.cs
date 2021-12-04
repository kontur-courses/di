using System.Drawing;

namespace TagsCloudDrawer.ImageSavior
{
    public interface IImageSavior
    {
        void Save(string filename, Image image);
    }
}