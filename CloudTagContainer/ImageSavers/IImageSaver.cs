using System.Drawing;
using System.IO;

namespace CloudTagContainer.ImageSavers
{
    public interface IImageSaver
    {
        void Save(Bitmap image, Stream outputStream);
    }
}