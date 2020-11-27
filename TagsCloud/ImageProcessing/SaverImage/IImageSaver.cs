using System.Drawing;

namespace TagsCloud.ImageProcessing.SaverImage
{
    public interface IImageSaver
    {
        public void SaveImageWithConfig(Bitmap bitmap, IImageConfig imageConfig);
    }
}
