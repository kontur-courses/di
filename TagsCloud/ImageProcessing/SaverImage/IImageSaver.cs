using System.Drawing;
using TagsCloud.ImageProcessing.Config;

namespace TagsCloud.ImageProcessing.SaverImage
{
    public interface IImageSaver
    {
        public void SaveImageWithConfig(Bitmap bitmap, IImageConfig imageConfig);
    }
}
