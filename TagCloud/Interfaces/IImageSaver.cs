using System.Drawing;

namespace TagCloud.Interfaces
{
    public interface IImageSaver
    {
        void Save(Image image, string path);
    }
}