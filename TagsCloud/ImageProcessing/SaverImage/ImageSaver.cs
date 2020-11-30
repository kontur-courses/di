using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.ImageProcessing.Config;

namespace TagsCloud.ImageProcessing.SaverImage
{

    public class ImageSaver : IImageSaver
    {
        public void SaveImageWithConfig(Bitmap bitmap, IImageConfig imageConfig)
        {
            var path = imageConfig.Path;
            if (!path.EndsWith(".png"))
                path += ".png";
            bitmap.Save(path, ImageFormat.Png);
        }
    }
}
