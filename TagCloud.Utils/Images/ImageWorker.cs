using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using TagCloud.Utils.Images.Interfaces;

namespace TagCloud.Utils.Images;

public class ImageWorker : IImageWorker
{
    public void SaveImage(Image image, string path, ImageFormat imageFormat, string filename)
    {
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var pathCombined = Path.Combine(path, filename + "." + imageFormat.ToString().ToLower());
        
        image.Save(pathCombined, imageFormat);
    }
}