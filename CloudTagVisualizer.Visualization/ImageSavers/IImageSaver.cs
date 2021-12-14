using System.Drawing;
using System.IO;

namespace Visualization.ImageSavers
{
    public interface IImageSaver
    {
        void Save(Bitmap image, Stream outputStream);
    }
}