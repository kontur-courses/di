using System.Drawing;

namespace TagCloud.Saver
{
    public interface IImageSaver
    {
        void Save(Image image, string fileName);
    }
}