using System.Drawing;
using System.Drawing.Imaging;
using TagsCloud.Interfaces;

namespace TagsCloud.FinalProcessing
{
    public class ImageSaver : IImageSaver
    {
        public void SaveImage(Image image, string path, ImageFormat formatResult)
        {
            image.Save(path, formatResult);
        }
    }
}
