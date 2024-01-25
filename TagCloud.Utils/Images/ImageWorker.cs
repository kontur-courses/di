using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using TagCloud.Utils.Images.Interfaces;

namespace TagCloud.Utils.Images;

public class ImageWorker : IImageWorker
{
    public void SaveImage(Image image, string path, string filename, ImageFormat? imageFormat = null)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        imageFormat ??= ImageFormat.Png;
        
        image.Save(Path.Combine(path, filename), imageFormat);
    }
}