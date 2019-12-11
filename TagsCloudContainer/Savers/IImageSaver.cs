using System.Drawing;

namespace TagsCloudContainer.Savers
{
    public interface IImageSaver
    {
        void Save(string path, Image image);
    }
}