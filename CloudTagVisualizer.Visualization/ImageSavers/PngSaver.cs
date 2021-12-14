using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Visualization.ImageSavers
{
    public class PngSaver : IImageSaver
    {
        public void Save(Bitmap image, Stream outputStream)
        {
            image.Save(outputStream, ImageFormat.Png);
        }
    }
}