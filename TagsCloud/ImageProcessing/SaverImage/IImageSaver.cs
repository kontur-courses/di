using System.Drawing;
using TagsCloud.ImageProcessing.Config;

namespace TagsCloud.ImageProcessing.SaverImage
{
    public interface IImageSaver
    {
        void SaveImageWithConfig(Bitmap bitmap, IImageConfig imageConfig);
    }
}
