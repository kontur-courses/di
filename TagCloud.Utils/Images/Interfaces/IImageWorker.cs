using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace TagCloud.Utils.Images.Interfaces;

public interface IImageWorker
{
    public void SaveImage(Image image, string path, string filename = "cloud.png", ImageFormat? imageFormat = null);
}